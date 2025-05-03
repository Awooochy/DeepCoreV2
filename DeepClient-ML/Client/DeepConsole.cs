using System;
using System.IO;
using System.Runtime.InteropServices;
using DeepCore.Client.Misc;
using DeepCore.Client.UI.QM;
using MelonLoader;

namespace DeepCore.Client
{
    internal class DeepConsole
    {
        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();
        public static void Alloc()
        {
            if (File.Exists("Plugins/IpcPlugin.dll"))
            {
                MelonLogger.Msg("Can't alloc console cause of eac.");
            }
            if (File.Exists("Plugins/ConsolePlugin.dll"))
            {
                MelonLogger.Msg("Can't alloc console cause of ConsolePlugin.dll.");
            }
            else
            {
                AllocConsole();
                TextWriter writer = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
                Console.SetOut(writer);
                Console.SetError(writer);
                Console.CursorVisible = false;
                Console.Title = "DeepCore - v2.0.5.B - Private";
            }
        }
        public static void Log(string Name, string Content)
        {
            DateTime now = DateTime.Now;
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(now.ToString("HH:mm"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] [");
            if (Name.StartsWith("Server", StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.Write(Name);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"] {Content}\n");
            Console.ResetColor();
            UI.QM.QMConsole.PrintLog(Name, Content);
        }
        public static void LogConsole(string Name, string Content)
        {
            DateTime now = DateTime.Now;
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(now.ToString("HH:mm"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] [");
            if (Name.StartsWith("Server", StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            Console.Write(Name);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"] {Content}\n");
            Console.ResetColor();
        }
        public static void LogException(Exception ex)
        {
            DateTime now = DateTime.Now;
            Console.ResetColor();
            string EXMSG = $"\n============ERROR============\nTIME:{(now.ToString("HH:mm"))}\nERROR MESSAGE:{ex.Message}\nLAST INSTRUCTIONS:{ex.StackTrace}\nFULL ERROR:{ex}\n=============END=============\n";
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(ex);
            Console.ResetColor();
        }
        public static void DebugLog(string msg)
        {
            DateTime now = DateTime.Now;
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(now.ToString("HH:mm"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"============DEBUG============{msg}\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(now.ToString("HH:mm"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("=============END=============\n");
            Console.ResetColor();
        }
        public static void Warn(string msg)
        {
            DateTime now = DateTime.Now;
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(now.ToString("HH:mm"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.Write($"============Warn==========={msg}\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(now.ToString("HH:mm"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Report this warning to the dev.\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(now.ToString("HH:mm"));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("] ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("=============END=============\n");
        }
        public static void ChangeTittle(string Name)
        {
            Console.Title = Name;
        }
        public static void Art()
        {
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Magenta;
                System.Console.WriteLine(":::::::-.  .,::::::  .,::::::  ::::::::::.       .,-:::::      ...     :::::::..   .,::::::  ");
                System.Console.WriteLine(" ;;,   `';,;;;;''''  ;;;;''''   `;;;```.;;;    ,;;;'````'   .;;;;;;;.  ;;;;``;;;;  ;;;;''''  ");
                System.Console.WriteLine(" `[[     [[ [[cccc    [[cccc     `]]nnn]]'     [[[         ,[[     |[[, [[[,/[[['   [[cccc   ");
                System.Console.WriteLine("  $$,    $$ $$^^^^    $$^^^^      $$$^^        $$$         $$$,     $$$ $$$$$$c     $$^^^^   ");
                System.Console.WriteLine("  888_,o8P' 888oo,__  888oo,__    888o         `88bo,__,o, ^888,_ _,88P 888b ^88bo, 888oo,__ ");
                System.Console.WriteLine("  MMMMP^    MMMMMMMMM MMMMYUMMM   YMMMb          \\YUMMMMMP  ^YMMMMMP^  MMMM   ^WMA MMMMYUMMM");
                System.Console.WriteLine("                                    :::      .::.  .:::.                                     ");
                System.Console.WriteLine("                                    ';;,   ,;;;'  ,;'``;.                                    ");
                System.Console.WriteLine("                                     |[[  .[[/    ''  ,[['                                   ");
                System.Console.WriteLine("                                      Y$c.$$\"     .c$$P'                                    ");
                System.Console.WriteLine("                                       Y88P      d88^   ,o,                                  ");
                System.Console.WriteLine("                                        MP       MMMUP*\"MMM,                                ");
                System.Console.WriteLine("---------------------------------------------------------------------------------------------");
            }
        }
    }
}
