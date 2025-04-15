using System.Diagnostics;

namespace DeepCore.Client.Module.ApplicationBot
{
    internal class BotAPI
    {
        public static void LaunchBot(string[] args)
        {
            string text = "";
            foreach (string str in args)
            {
                text = text + " " + str;
            }
            new Process
            {
                StartInfo =
                {
                    FileName = "VRChat.exe",
                    Arguments = text,
                    UseShellExecute = true
                }
            }.Start();
        }
    }
}
