using System;
using System.IO;
using System.Runtime.InteropServices;

namespace DeepClient.Client
{
    internal class DeepConsole
    {
         [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool FreeConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();
        public static bool BeShitConsole = false;
        public static void Alloc()
        {
            {
                IntPtr consoleWindow = GetConsoleWindow();
                if (consoleWindow == IntPtr.Zero)
                {
                    AllocConsole();
                    TextWriter writer = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
                    Console.SetOut(writer);
                    Console.SetError(writer);
                    Console.CursorVisible = false;
                    Console.Title = "DeepConsole";
                    Art(false);
                    Log("Console", "Allocated console :3");
                }
                else
                {
                    BeShitConsole = true;
                    Log("Console", "Oh hell nah you are using be one.");
                }
            }
        }
        public static void Log(string Name, string Content)
        {
            if (BeShitConsole)
            {
                EntryPoint.Log.LogMessage($"[{Name}] " + Content);
                ClientMenu.QMConsole.PrintLog(Name, Content);
            }
            else 
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
            ClientMenu.QMConsole.PrintLog(Name, Content);
            }
        }
        public static void LogConsole(string Name, string Content)
        {
            if (BeShitConsole)
            {
                EntryPoint.Log.LogMessage($"[{Name}] " +Content);
            }
            else 
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