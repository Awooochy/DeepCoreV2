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
                Console.Title = "DeepClient - v0.0.5 - Private";
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
            else if (Name.StartsWith("Bot", StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
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
            else if (Name.StartsWith("Bot", StringComparison.OrdinalIgnoreCase))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
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
        public static void E(Exception ex)
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
        public static void Art(bool s)
        {
            if (s)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($@"
       ________                      __________        __   
       \______ \   ____   ____ ______\______   \ _____/  |_ 
       |    |  \_/ __ \_/ __ \\____ \|    |  _//  _ \   __\
       |    `   \  ___/\  ___/|  |_> >    |   (  <_> )  |  
       /_______  /\___  >\___  >   __/|______  /\____/|__|  
               \/     \/     \/|__|          \/             

-    When the sun explodes, all I did will be for nothing~   -
 -                        Running v0.0.5                    -
  -                https://discord.gg/SKhrH4C8K6           -
");
            }
            else
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($@"
________                       _________ .__  .__               __   
\______ \   ____   ____ ______ \_   ___ \|  | |__| ____   _____/  |_ 
 |    |  \_/ __ \_/ __ \\____ \/    \  \/|  | |  |/ __ \ /    \   __\
 |    `   \  ___/\  ___/|  |_> >     \___|  |_|  \  ___/|   |  \  |  
/_______  /\___  >\___  >   __/ \______  /____/__|\___  >___|  /__|  
        \/     \/     \/|__|           \/             \/     \/      

-    When the sun explodes, all I did will be for nothing~    -
 -                        Running v0.0.5                     -
  -                https://discord.gg/SKhrH4C8K6            -
");
            }
        }
    }
}
