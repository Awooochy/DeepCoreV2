using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VRC.Udon;
using VRC.Udon.VM;
using VRC.Udon.VM.Common;

namespace DeepCore.Client.Misc
{
    public static class UdonDisassembler
    {
        private static readonly string SaveDir = Path.Combine(Directory.GetCurrentDirectory(), "DeepClient/UdonDisassembler");

        public static void Disassemble(UdonBehaviour udonBehaviour, string ubName)
        {
            Disassemble(udonBehaviour, ubName, null);
        }
        public static void Disassemble(UdonBehaviour udonBehaviour, string ubName, IEnumerable<string> targetEventNames)
        {
            try
            {
                var udonProgram = udonBehaviour._program;
                var byteCode = udonProgram.ByteCode;
                var symbolTable = udonProgram.SymbolTable;
                var heap = udonProgram.Heap;
                var eventTable = udonBehaviour._eventTable;

                var output = new StringBuilder();
                var constants = new Dictionary<uint, string>();

                HashSet<uint> eventAddresses = new HashSet<uint>();
                Dictionary<uint, string> addressToEventName = new Dictionary<uint, string>();

                bool filterEvents = targetEventNames != null && targetEventNames.Any();
                HashSet<string> eventNameFilter = filterEvents
                    ? new HashSet<string>(targetEventNames, StringComparer.OrdinalIgnoreCase)
                    : null;

                if (eventTable != null)
                {
                    foreach (var entry in eventTable)
                    {
                        if (filterEvents && !eventNameFilter.Contains(entry.key))
                            continue;

                        foreach (var addr in entry.Value)
                        {
                            eventAddresses.Add(addr);
                            addressToEventName[addr] = entry.key;
                        }
                    }
                }

                if (filterEvents && eventAddresses.Count == 0)
                {
                    DeepConsole.Warn($"No matching events found for the specified filter in {ubName}");
                    return;
                }

                Dictionary<uint, uint> eventBounds = new Dictionary<uint, uint>();
                if (filterEvents)
                {
                    var sortedAddresses = eventAddresses.OrderBy(a => a).ToList();
                    for (int i = 0; i < sortedAddresses.Count; i++)
                    {
                        uint startAddr = sortedAddresses[i];
                        uint endAddr = (i < sortedAddresses.Count - 1)
                            ? sortedAddresses[i + 1]
                            : (uint)byteCode.Length;

                        eventBounds[startAddr] = endAddr;
                    }
                }

                int address = 0;
                bool isProcessingTargetEvent = !filterEvents;
                uint currentEventEndAddress = (uint)byteCode.Length;

                while (address < byteCode.Length)
                {
                    try
                    {
                        if (eventAddresses.Contains((uint)address))
                        {
                            string eventName = addressToEventName[(uint)address];

                            if (filterEvents)
                            {
                                isProcessingTargetEvent = true;
                                currentEventEndAddress = eventBounds[(uint)address];
                            }

                            output.AppendLine(".end_of_func");
                            output.AppendLine($".func_{eventName}");
                        }

                        if (!isProcessingTargetEvent)
                        {
                            address += 4;
                            continue;
                        }

                        if (filterEvents && address >= currentEventEndAddress)
                        {
                            isProcessingTargetEvent = false;
                            continue;
                        }

                        if (address + 4 > byteCode.Length)
                        {
                            DeepConsole.Warn($"Reached end of bytecode prematurely at 0x{address:X}.");
                            break;
                        }

                        uint opcode = SwapEndianness(BitConverter.ToUInt32(byteCode, address));

                        switch (opcode)
                        {
                            case (uint)OpCode.NOP:
                                DeepConsole.DebugLog("\nResolving NOP");
                                output.AppendLine($"0x{address:X}  NOP");
                                address += 4;
                                break;

                            case (uint)OpCode.PUSH:
                                if (address + 8 > byteCode.Length) break;

                                DeepConsole.DebugLog("\nResolving PUSH");

                                uint pushOffset = SwapEndianness(BitConverter.ToUInt32(byteCode, address + 4));

                                string symbolName = symbolTable.GetSymbolFromAddress(pushOffset) ?? $"0x{pushOffset:X}";
                                object heapValue = heap.GetHeapVariable(pushOffset);
                                string symbolType = heapValue?.GetType().FullName ?? "Unknown";

                                if (heapValue != null && symbolName.Contains("_const_"))
                                {
                                    Type runtimeType = heapValue.GetType();

                                    if (runtimeType == typeof(bool) ||
                                        runtimeType == typeof(int) ||
                                        runtimeType == typeof(float) ||
                                        runtimeType == typeof(double))
                                    {
                                        constants[pushOffset] = heapValue.ToString();
                                    }
                                    else if (runtimeType == typeof(string))
                                    {
                                        constants[pushOffset] = $"\"{heapValue}\"";
                                    }
                                }

                                output.AppendLine($"0x{address:X}  PUSH 0x{pushOffset:X} ({symbolName}[{symbolType}])");
                                address += 8;
                                break;

                            case (uint)OpCode.POP:
                                DeepConsole.DebugLog("\nResolving POP");
                                output.AppendLine($"0x{address:X}  POP");
                                address += 4;
                                break;

                            case (uint)OpCode.JUMP_IF_FALSE:
                                if (address + 8 > byteCode.Length) break;

                                DeepConsole.DebugLog("\nResolving JUMP_IF_FALSE");
                                uint jneOffset = SwapEndianness(BitConverter.ToUInt32(byteCode, address + 4));
                                output.AppendLine($"0x{address:X}  JUMP_IF_FALSE 0x{jneOffset:X}");
                                address += 8;
                                break;

                            case (uint)OpCode.JUMP:
                                if (address + 8 > byteCode.Length) break;

                                DeepConsole.DebugLog("\nResolving JMP");

                                uint jumpOffset = SwapEndianness(BitConverter.ToUInt32(byteCode, address + 4));
                                output.AppendLine($"0x{address:X}  JMP 0x{jumpOffset:X}");
                                address += 8;
                                break;

                            case (uint)OpCode.EXTERN:
                                if (address + 8 > byteCode.Length) break;

                                DeepConsole.DebugLog("\nResolving EXTERN");

                                uint externAddr = SwapEndianness(BitConverter.ToUInt32(byteCode, address + 4));
                                object externObj = heap.GetHeapVariable(externAddr);
                                string externName = $"unknown_extern@{externAddr:X}";

                                if (externObj != null)
                                {
                                    if (externObj is UdonVM.CachedUdonExternDelegate cachedDelegate)
                                    {
                                        externName = cachedDelegate.externSignature ?? externName;
                                    }
                                    else
                                    {
                                        externName = externObj.ToString();
                                    }
                                }

                                output.AppendLine($"0x{address:X}  EXTERN \"{externName}\"");
                                address += 8;
                                break;

                            case (uint)OpCode.ANNOTATION:
                                if (address + 8 > byteCode.Length) break;

                                DeepConsole.DebugLog("\nResolving ANNOTATION");

                                uint annotationAddr = SwapEndianness(BitConverter.ToUInt32(byteCode, address + 4));
                                object annotationObj = heap.GetHeapVariable(annotationAddr);
                                string annotationText = annotationObj?.ToString() ?? $"unknown_annotation@{annotationAddr:X}";

                                output.AppendLine($"0x{address:X}  ANNOTATION \"{annotationText}\"");
                                address += 8;
                                break;

                            case (uint)OpCode.JUMP_INDIRECT:
                                if (address + 8 > byteCode.Length) break;

                                DeepConsole.DebugLog("\nResolving JMP_INDIRECT");

                                uint indirectAddr = SwapEndianness(BitConverter.ToUInt32(byteCode, address + 4));
                                string targetSymbol = symbolTable.HasSymbolForAddress(indirectAddr) ?
                                    symbolTable.GetSymbolFromAddress(indirectAddr) : $"0x{indirectAddr:X}";

                                output.AppendLine($"0x{address:X}  JMP [{targetSymbol}]");
                                address += 8;
                                break;

                            case (uint)OpCode.COPY:
                                DeepConsole.DebugLog("\nResolving COPY");
                                output.AppendLine($"0x{address:X}  COPY");
                                address += 4;
                                break;

                            default:
                                DeepConsole.Log("UdonDBLR", $"Unknown opcode: {opcode} at address 0x{address:X}");
                                address += 4;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        DeepConsole.Log("UdonDBLR",$"Failed to process bytecode at 0x{address:X}:{ex.Message}");
                        break;
                    }
                }
                output.AppendLine(".end");
                if (!Directory.Exists(SaveDir))
                {
                    Directory.CreateDirectory(SaveDir);
                }

                string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string filenameSuffix = filterEvents ? "_filtered" : "";
                string outputPath = Path.Combine(SaveDir, $"Disassembled_{ubName}{filenameSuffix}_{timestamp}.txt");
                DeepConsole.Log("UdonDBLR", "Saving to file...");
                File.WriteAllText(outputPath, output.ToString());

                if (constants.Count > 0)
                {
                    var constantsOutput = new StringBuilder(constants.Count * 20);
                    foreach (var kv in constants)
                    {
                        constantsOutput.AppendLine($"0x{kv.Key:X}: {kv.Value}");
                    }
                    string constantsPath = Path.Combine(SaveDir, $"Constants_{ubName}{filenameSuffix}_{timestamp}.txt");
                    File.WriteAllText(constantsPath, constantsOutput.ToString());
                }
                string eventInfo = filterEvents ? $"(filtered to {eventAddresses.Count} events)": "";
                DeepConsole.Log("UdonDBLR",$"Disassembly completed for {ubName}{eventInfo}");
            }
            catch (Exception ex)
            {
                DeepConsole.Log("UdonDBLR", $"Error in UdonDisassembler.");
                DeepConsole.E(ex);
                throw;
            }
        }
        private static uint SwapEndianness(uint value)
        {
            return ((value & 0x000000FF) << 24) |
                   ((value & 0x0000FF00) << 8) |
                   ((value & 0x00FF0000) >> 8) |
                   ((value & 0xFF000000) >> 24);
        }
    }
}
