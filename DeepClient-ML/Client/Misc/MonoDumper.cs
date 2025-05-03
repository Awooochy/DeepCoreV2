using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace DeepCore.Client.Misc
{
    internal class MonoDumper
    {
        public static bool DumpDLL = true;

        public static void StartDump()
        {
            Console.WriteLine("[MonoDumper] StartDump initiated.");

            if (!DumpDLL)
            {
                Console.WriteLine("[MonoDumper] DumpDLL is set to false. Exiting method.");
                return;
            }

            var dumpFolderPath = Environment.CurrentDirectory + @"\DeepCoreV2\MonoDumps";
            var dumpFilePath = dumpFolderPath + @"\MonoDump.txt";

            Console.WriteLine($"[MonoDumper] Dump folder path: {dumpFolderPath}");
            Console.WriteLine($"[MonoDumper] Dump file path: {dumpFilePath}");

            try
            {
                if (!Directory.Exists(dumpFolderPath))
                {
                    Directory.CreateDirectory(dumpFolderPath);
                    Console.WriteLine("[MonoDumper] Created dump folder.");
                }

                if (File.Exists(dumpFilePath))
                {
                    File.Delete(dumpFilePath);
                    Console.WriteLine("[MonoDumper] Deleted existing dump file.");
                }

                using (FileStream fs = new FileStream(dumpFilePath, FileMode.Create))
                {
                    Console.WriteLine("[MonoDumper] Created new dump file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[MonoDumper] Error handling dump files: {ex.Message}");
                return;
            }

            StringBuilder stringBuilder = new StringBuilder();

            try
            {
                string assemblyPath = Environment.CurrentDirectory + @"\MelonLoader\Managed\Assembly-CSharp.dll";
                Console.WriteLine($"[MonoDumper] Loading assembly from: {assemblyPath}");

                foreach (Type type in Assembly.LoadFile(assemblyPath).GetTypes())
                {
                    stringBuilder.Append("\n\n*********************************************************************");
                    stringBuilder.Append("Class: " + type.Name + Environment.NewLine);

                    Console.WriteLine($"[MonoDumper] Processing class: {type.Name}");

                    try
                    {
                        MemberInfo[] methods = type.GetMethods();
                        foreach (MemberInfo method in methods)
                        {
                            stringBuilder.Append("Method: " + method.Name + Environment.NewLine);
                            Console.WriteLine($"[MonoDumper] Found method: {method.Name}");
                        }

                        foreach (PropertyInfo property in type.GetProperties())
                        {
                            stringBuilder.Append("Property: " + property.Name + Environment.NewLine);
                            Console.WriteLine($"[MonoDumper] Found property: {property.Name}");
                        }
                    }
                    catch (Exception e)
                    {
                        stringBuilder.Append($"Failed: {e.Message}\n\n");
                        Console.WriteLine($"[MonoDumper] Error processing class members: {e.Message}");
                    }

                    stringBuilder.Append("*********************************************************************\n\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"[MonoDumper] Error loading assembly or iterating types: {e.Message}");
                return;
            }

            try
            {
                File.AppendAllText(dumpFilePath, stringBuilder.ToString());
                Console.WriteLine("[MonoDumper] Dump successfully written to file.");
            }
            catch (Exception e)
            {
                Console.WriteLine($"[MonoDumper] Error writing to dump file: {e.Message}");
            }

            Console.WriteLine("All dumped!");
            Console.ReadLine();
        }
    }
}