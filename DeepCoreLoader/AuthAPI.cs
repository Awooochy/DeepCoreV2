using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Collections.Specialized;
using MelonLoader;
using DeepCoreLoader;

namespace DeepLoader.ServerAPI
{
    internal class AuthAPI
    {
        private const string KeyFormat = "^KEY_[A-Za-z0-9]{60}$";
        private string Context;
        private bool isAuthenticated = false;
        private string AuthKey = "";
        private readonly string appDataFolder;
        private readonly string keyFilePath;
        private readonly string dllSavePath;

        public AuthAPI()
        {
            appDataFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DPC", "DeepCoreClient");
            keyFilePath = Path.Combine(appDataFolder, "TONTOELQUELOLEA");
            dllSavePath = Path.Combine(MelonUtils.BaseDirectory, "Mods/DeepCore.dll");

            if (!Directory.Exists(appDataFolder))
            {
                Directory.CreateDirectory(appDataFolder);
            }

            // Ensure Mods directory exists
            string modsDir = Path.Combine(MelonUtils.BaseDirectory, "Mods");
            if (!Directory.Exists(modsDir))
            {
                Directory.CreateDirectory(modsDir);
            }
        }

        public void awooochyWasHere()
        {
            String StopMessingWithMyClientYouWontFindAnyMalwareOrLoggerIsCleanNowGoAndCloseYourDecompiler = "Deja mi puto loader tranquilo, si sigues metiendo las narizes donde no debes quizas si encuentres algo que no te guste";
        }

        public bool Auth()
        {
            string AUTHhwid = GetHardwareID();
            string AUTHIPV4 = GetIPv4Address();
            string AUTHIPV6 = GetIPv6Address(); // New: Fetch IPv6 address
            string loaderToken =
                "VerifiedLoader_IZ7xjF5X1X9U4DQ2I8X9735z16dj86041L7a74g4Dtvwikz1eky4Vdbq64kV";

            bool keyExists = File.Exists(keyFilePath);

            if (!keyExists)
            {
                // First-time authorization
                Console.WriteLine("Enter your Key: ");
                AuthKey = Console.ReadLine().Trim();

                if (!Regex.IsMatch(AuthKey, KeyFormat))
                {
                    MelonLogger.Msg("Invalid Key format. Authentication failed.");
                    Thread.Sleep(3000);
                    Environment.Exit(0);
                }
                

                if (!ValidateKey(AuthKey, AUTHIPV4, AUTHIPV6, AUTHhwid, loaderToken))
                {
                    ReportUnauthorizedAttempt(AuthKey);
                    Environment.Exit(0);
                }

                File.WriteAllText(keyFilePath, AuthKey);
            }
            else
            {
                MelonLogger.Msg("Connecting to server...");
                AuthKey = File.ReadAllText(keyFilePath).Trim();
                

                if (!Regex.IsMatch(AuthKey, KeyFormat))
                {
                    MelonLogger.Msg("Invalid Key format in key file. Authentication failed.");
                    Thread.Sleep(3000);
                    Environment.Exit(0);
                }

                if (!ValidateKey(AuthKey, AUTHIPV4, AUTHIPV6, AUTHhwid, loaderToken))
                {
                    ReportUnauthorizedAttempt(AuthKey);
                    Environment.Exit(0);
                }
            }

            if (isAuthenticated)
            {
                ReportSuccessfulAuth(AuthKey);
                MelonLogger.Msg("Authentication successful");
                string username = GetUsernameFromKey(AuthKey);
                MelonLogger.Msg($"Welcome, {username}!");
                Thread.Sleep(3000);
                return true; // Return success status
            }
            return false; // Return failure status
        }

        private bool ValidateKey(string key, string ipv4, string ipv6, string hwid, string loaderToken)
        {
            string validationUrl = "https://nigga.rest/where/is/biden/AUTHER/KeyValidator.php";
            var keyValidationData = new NameValueCollection
            {
                { "key", key },
                { "ipv4", ipv4 },
                { "ipv6", ipv6 },
                { "hwid", hwid },
                { "loaderToken", loaderToken }
            };

            using (var client = new WebClient())
            {
                try
                {
                    byte[] responseBytes = client.UploadValues(validationUrl, "POST", keyValidationData);
                    string responseString = Encoding.UTF8.GetString(responseBytes);
    
                    try
                    {
                        var jsonResponse = JObject.Parse(responseString);
        
                        // Handle DLL response with more robust checks
                        if (jsonResponse["status"]?.ToString() == "dll")
                        {
                            string base64Data = jsonResponse["data"]?.ToString();
                            if (string.IsNullOrEmpty(base64Data) || !base64Data.StartsWith("DLL:"))
                            {
                                MelonLogger.Error("Invalid DLL data received");
                                return false;
                            }

                            try 
                            {
                                byte[] dllBytes = Convert.FromBase64String(base64Data.Substring(4));
                
                                // Additional integrity checks
                                if (dllBytes == null || dllBytes.Length == 0)
                                {
                                    MelonLogger.Error("Received empty DLL bytes");
                                    return false;
                                }

                                // Ensure directory exists
                                string dllDirectory = Path.GetDirectoryName(dllSavePath);
                                if (!Directory.Exists(dllDirectory))
                                {
                                    Directory.CreateDirectory(dllDirectory);
                                }

                                // Save DLL with additional error handling
                                //MelonLogger.Msg("Saving DLL file...");
                                File.WriteAllBytes(dllSavePath, dllBytes);
                
                                // Verify file was actually saved
                                int maxAttempts = 5;
                                int attempt = 0;
                                bool fileSaved = false;
                        
                                while (attempt < maxAttempts && !fileSaved)
                                {
                                    if (File.Exists(dllSavePath))
                                    {
                                        // Additional verification that file is not empty
                                        FileInfo fileInfo = new FileInfo(dllSavePath);
                                        if (fileInfo.Length > 0)
                                        {
                                            fileSaved = true;
                                            break;
                                        }
                                    }
                            
                                    attempt++;
                                    MelonLogger.Msg($"Waiting for client load... (Attempt {attempt}/{maxAttempts})");
                                    Thread.Sleep(1000); // Wait 1 second between checks
                                }

                                if (!fileSaved)
                                {
                                    
                                    
                                    MelonLogger.Error($"Failed to load client after {maxAttempts} attempts");
                                    return false;
                                }

                                //MelonLogger.Msg($"DLL saved successfully to {dllSavePath}");
                                isAuthenticated = true;
                                return true;
                            }
                            catch (Exception ex)
                            {
                                //MelonLogger.Error($"DLL saving failed: {ex.Message}");
                                //MelonLogger.Error($"Full stack trace: {ex.StackTrace}");
                                return false;
                            }
                        }
        
                        // Handle normal auth response
                        if (jsonResponse["status"]?.ToString() == "authenticated")
                        {
                            isAuthenticated = true;
                            return true;
                        }
        
                        MelonLogger.Msg(jsonResponse["message"]?.ToString() ?? "Unknown error");
                        return false;
                    }
                    catch (Exception parseEx)
                    {
                        MelonLogger.Error($"Response parsing error: {parseEx.Message}");
                        return false;
                    }
                }
                catch (WebException ex)
                {
                    MelonLogger.Error($"Network error: {ex.Message}");
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
            string message = $"Unauthorized key attempt: HWID: {hwid}, IPV4: {IPV4}, IPV6: {IPV6}, PC: {PC}, USER: {USER}, Auth Key: {key}, Context: {Context}";
            SendDiscordWebhookNoAuth(message);
            MelonLogger.Msg("Unauthorized key/banned user or server is unreachable");
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
                    MelonLogger.Msg("Fatal error");
                    Thread.Sleep(1000);
                    MelonLogger.Msg("Preventing application crash...");
                    Thread.Sleep(3000);
                    MelonLogger.Msg("Closing in safe mode...");
                    Thread.Sleep(3000);
                    Environment.Exit(0);
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
                    MelonLogger.Msg("Fatal error");
                    Thread.Sleep(1000);
                    MelonLogger.Msg("Preventing application crash...");
                    Thread.Sleep(3000);
                    MelonLogger.Msg("Closing in safe mode...");
                    Thread.Sleep(3000);
                    Environment.Exit(0);
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
                catch (WebException eX)
                {
                    MelonLogger.Msg("Error connecting to the server for key validation. ERROR RETRIEVING USERNAME");
                    Context = "ERROR RETRIEVING USERNAME FROM SERVER?";
                    MelonLogger.Msg(eX);
                    Thread.Sleep(3000);
                    Environment.Exit(0);
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
                    MelonLogger.Msg("ERROR FETCHING IP");
                    Environment.Exit(1);
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
                    MelonLogger.Msg("[IGNORE THIS IF YOU DON'T HAVE IPV6] ERROR FETCHING IPV6");
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