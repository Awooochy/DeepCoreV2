using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using DeepCore.Client;
using System.Diagnostics;

namespace DeepCore.ServerAPI
{
    internal class AuthAPI
    {
        private const string KeyFormat = "^KEY_[A-Za-z0-9]{60}$";
        private bool isAuthenticated = false;
        private string AuthKey = "";
        private readonly string appDataFolder;
        private readonly string keyFilePath;

        public AuthAPI()
        {
            appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DPC", "DeepCoreClient");
            keyFilePath = Path.Combine(appDataFolder, "TONTOELQUELOLEA");

            if (!Directory.Exists(appDataFolder))
            {
                Directory.CreateDirectory(appDataFolder);
            }
        }

        public void Auth()
        {
            string AUTHhwid = GetHardwareID();
            string AUTHIPV4 = GetIPv4Address();
            string AUTHIPV6 = GetIPv6Address(); // New: Fetch IPv6 address

            bool keyExists = File.Exists(keyFilePath);

            if (!keyExists)
            {
                Console.Clear();
                DeepConsole.LogConsole("Auth","Enter your Key: ");
                AuthKey = Console.ReadLine().Trim();

                if (!Regex.IsMatch(AuthKey, KeyFormat))
                {
                    DeepConsole.LogConsole("Auth", "Invalid Key format. Authentication failed.");
                    Thread.Sleep(3000);
                    Process.GetCurrentProcess().Kill();
                }

                if (!ValidateKey(AuthKey, AUTHIPV4, AUTHIPV6, AUTHhwid))
                {
                    ReportUnauthorizedAttempt(AuthKey);
                    Process.GetCurrentProcess().Kill();
                }

                File.WriteAllText(keyFilePath, AuthKey);
            }
            else
            {
                DeepConsole.LogConsole("Auth", "Connecting to server...");
                AuthKey = File.ReadAllText(keyFilePath).Trim();

                if (!Regex.IsMatch(AuthKey, KeyFormat))
                {
                    DeepConsole.LogConsole("Auth", "Invalid Key format in key file. Authentication failed.");
                    Thread.Sleep(3000);
                    Process.GetCurrentProcess().Kill();
                }

                if (!ValidateKey(AuthKey, AUTHIPV4, AUTHIPV6, AUTHhwid))
                {
                    ReportUnauthorizedAttempt(AuthKey);
                    Process.GetCurrentProcess().Kill();
                }
            }

            if (isAuthenticated)
            {
                ReportSuccessfulAuth(AuthKey);
                DeepConsole.LogConsole("Auth", "Authentication successful");
                string username = GetUsernameFromKey(AuthKey);
                DeepConsole.LogConsole("Auth", $"Welcome, {username}!");
                Thread.Sleep(3000);
            }
        }

        private bool ValidateKey(string key, string ipv4, string ipv6, string hwid)
        {
            string validationUrl = "https://nigga.rest/where/is/biden/AUTHER/KeyValidator.php";
            var keyValidationData = new NameValueCollection
            {
                { "key", key },
                { "ipv4", ipv4 },
                { "ipv6", ipv6 }, // New: Send IPv6
                { "hwid", hwid }
            };

            using (var client = new WebClient())
            {
                try
                {
                    byte[] responseBytes = client.UploadValues(validationUrl, "POST", keyValidationData);
                    string responseString = Encoding.UTF8.GetString(responseBytes);
                    var jsonResponse = JObject.Parse(responseString);

                    if (jsonResponse["status"].ToString() == "authenticated")
                    {
                        isAuthenticated = true;
                        return true;
                    }
                    else
                    {
                        Console.WriteLine(jsonResponse["message"].ToString());
                        return false;
                    }
                }
                catch (WebException)
                {
                    DeepConsole.LogConsole("Auth", "Error connecting to the server for key validation.");
                    Thread.Sleep(3000);
                    return false;
                }
            }
        }

        private void ReportUnauthorizedAttempt(string key)
        {
            string hwid = GetHardwareID();
            string IPV4 = GetIPv4Address();
            string IPV6 = GetIPv6Address(); // New: Include IPv6
            string PC = GetComputerName();
            string USER = GetUserName();
            string message = $"Unauthorized key attempt: HWID: {hwid}, IPV4: {IPV4}, IPV6: {IPV6}, PC: {PC}, USER: {USER}, Auth Key: {key}";
            SendDiscordWebhookNoAuth(message);
            DeepConsole.LogConsole("Auth", "Unauthorized key/banned user or server is unreachable");
            Thread.Sleep(3000);
        }

        private void ReportSuccessfulAuth(string key)
        {
            string hwid = GetHardwareID();
            string IPV4 = GetIPv4Address();
            string IPV6 = GetIPv6Address(); // New: Include IPv6
            string PC = GetComputerName();
            string USER = GetUserName();
            string message = $"Authentication successful: HWID: {hwid}, IPV4: {IPV4}, IPV6: {IPV6}, PC: {PC}, USER: {USER}, Auth Key: {key}";
            SendDiscordWebhook(message);
        }

        private void SendDiscordWebhook(string message)
        {
            string apiUrl = "https://nigga.rest/where/is/biden/AUTHER/WBHSender.php";
            var webhookData = new NameValueCollection
            {
                { "type", "auth" },
                { "message", message }
            };

            using (var client = new WebClient())
            {
                try
                {
                    client.UploadValues(apiUrl, "POST", webhookData);
                }
                catch (WebException)
                {
                    DeepConsole.LogConsole("Auth", "Fatal error");
                    Thread.Sleep(1000);
                    DeepConsole.LogConsole("Auth", "Preventing application crash...");
                    Thread.Sleep(3000);
                    DeepConsole.LogConsole("Auth", "Closing in safe mode...");
                    Thread.Sleep(3000);
                    Process.GetCurrentProcess().Kill();
                }
            }
        }

        private void SendDiscordWebhookNoAuth(string message)
        {
            string apiUrl = "https://nigga.rest/where/is/biden/AUTHER/WBHSender.php";
            var webhookData = new NameValueCollection
            {
                { "type", "noAuth" },
                { "message", message }
            };

            using (var client = new WebClient())
            {
                try
                {
                    client.UploadValues(apiUrl, "POST", webhookData);
                }
                catch (WebException)
                {
                    DeepConsole.LogConsole("Auth", "Fatal error");
                    Thread.Sleep(1000);
                    DeepConsole.LogConsole("Auth", "Preventing application crash...");
                    Thread.Sleep(3000);
                    DeepConsole.LogConsole("Auth", "Closing in safe mode...");
                    Thread.Sleep(3000);
                    Process.GetCurrentProcess().Kill();
                }
            }
        }

        private string GetUsernameFromKey(string key)
        {
            string validationUrl = "https://nigga.rest/where/is/biden/AUTHER/UsernameManager.php";
            var keyValidationData = new NameValueCollection
            {
                { "key", key }
            };

            using (var client = new WebClient())
            {
                try
                {
                    byte[] responseBytes = client.UploadValues(validationUrl, "POST", keyValidationData);
                    return Encoding.UTF8.GetString(responseBytes);
                }
                catch (WebException)
                {
                    DeepConsole.LogConsole("Auth", "Error connecting to the server for key validation.");
                    Thread.Sleep(3000);
                    Process.GetCurrentProcess().Kill();
                    return null;
                }
            }
        }

        public static string GetHardwareID()
        {
            string location = @"SOFTWARE\Microsoft\Cryptography";
            string name = "MachineGuid";

            using (RegistryKey localMachineX64View = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey rk = localMachineX64View.OpenSubKey(location))
                {
                    if (rk == null)
                        throw new KeyNotFoundException($"Key Not Found: {location}");

                    object machineGuid = rk.GetValue(name);
                    if (machineGuid == null)
                        throw new IndexOutOfRangeException($"Index Not Found: {name}");

                    return machineGuid.ToString();
                }
            }
        }

        public static string GetIPv4Address()
        {
            string apiUrl = "https://api.ipify.org?format=text";

            using (var client = new WebClient())
            {
                try
                {
                    string publicIpAddress = client.DownloadString(apiUrl);
                    return publicIpAddress.Trim();
                }
                catch (WebException)
                {
                    DeepConsole.LogConsole("Auth", "ERROR FETCHING IP");
                    Process.GetCurrentProcess().Kill();
                    return "Unknown";
                }
            }
        }

        public static string GetIPv6Address()
        {
            string apiUrl = "https://api6.ipify.org?format=text"; // New: Fetch IPv6 address

            using (var client = new WebClient())
            {
                try
                {
                    string publicIpAddress = client.DownloadString(apiUrl);
                    return publicIpAddress.Trim();
                }
                catch (WebException)
                {
                    DeepConsole.LogConsole("Auth", "ERROR FETCHING IPV6");
                    return "Unknown";
                }
            }
        }

        public static string GetComputerName()
        {
            return Environment.MachineName;
        }

        public static string GetUserName()
        {
            return Environment.UserName;
        }
    }
}
