using System.Net.Http;
using System;
using MelonLoader;
using System.IO;
using DeepLoader.ServerAPI;
using System.Threading.Tasks;
using System.Collections.Generic;



namespace DeepCoreLoader
{
    public class Main : MelonPlugin
    {
        private static byte[] _dllBytes;
        private const string VersionFile = "DPVersionControlFile.txt";
        private const string VersionCheckUrl = "https://nigga.rest/where/is/biden/VersionCheck.php";
        private readonly string _dllPath = Path.Combine(MelonUtils.BaseDirectory, "Mods", "DeepCore.dll");
        private readonly string _versionFilePath = Path.Combine(MelonUtils.BaseDirectory, VersionFile);

        public override void OnPreModsLoaded()
        {
            // Ensure Mods directory exists
            Directory.CreateDirectory(Path.Combine(MelonUtils.BaseDirectory, "Mods"));

            // Create version file if it doesn't exist
            if (!File.Exists(_versionFilePath))
            {
                File.WriteAllText(_versionFilePath, "0.0.0");
                MelonLogger.Msg("Created new version control file with version 0.0.0");
            }

            if (!File.Exists(_dllPath))
            {
                MelonLogger.Msg("DeepCore.dll not found, initiating authentication...");
                try
                {
                    AuthAPI authAPI = new AuthAPI();
                    if (authAPI.Auth()) // Modified Auth() to return bool
                    {
                        // Only proceed if auth was successful and DLL was downloaded
                        _dllBytes = File.ReadAllBytes(_dllPath);
                    }
                }
                catch (Exception problem)
                {
                    MelonLogger.Msg("Something failed during auth API" + problem);
                }
                
            }
            else
            {
                CheckVersionAndUpdate();
            }
        }

        private async void CheckVersionAndUpdate()
        {
            try
            {
                string localVersion = File.ReadAllText(_versionFilePath).Trim();
                string serverVersion = await GetServerVersion();
                MelonLogger.Msg($"Local version: {localVersion}, Server version: {serverVersion}");

                if (IsVersionNewer(serverVersion, localVersion))
                {
                    MelonLogger.Msg("New version available, initiating update...");
                    AuthAPI authAPI = new AuthAPI();
                    if (authAPI.Auth()) // Modified Auth() to return bool
                    {
                        // Update version file
                        File.WriteAllText(_versionFilePath, serverVersion);
                        _dllBytes = File.ReadAllBytes(_dllPath);
                        MelonLogger.Msg($"Updated to version {serverVersion}");
                    }
                }
                else
                {
                    MelonLogger.Msg("Using current version, no update needed.");
                    LoadExistingDll();
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Version check failed: {ex.Message}");
                LoadExistingDll();
            }
        }

        private async Task<string> GetServerVersion()
        {
            using (HttpClient client = new HttpClient())
            {
                // Prepare the URL with the query parameter
                var requestUrl = $"{VersionCheckUrl}?keyword=versioncheck";

                // Send the GET request
                HttpResponseMessage response = await client.GetAsync(requestUrl);
                response.EnsureSuccessStatusCode();
        
                // Read and return the response content
                return await response.Content.ReadAsStringAsync();
            }
        }


        private bool IsVersionNewer(string serverVersion, string localVersion)
        {
            try
            {
                Version server = new Version(serverVersion);
                Version local = new Version(localVersion);
                return server > local;
            }
            catch
            {
                // If version parsing fails, assume we need update
                return true;
            }
        }

        private void LoadExistingDll()
        {
            try
            {
                if (File.Exists(_dllPath))
                {
                    _dllBytes = File.ReadAllBytes(_dllPath);
                    MelonLogger.Msg("Successfully loaded existing DeepCore.dll");
                }
                else
                {
                    MelonLogger.Error("DLL file not found at expected path");
                    _dllBytes = null;
                }
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Failed to load existing DLL: {ex.Message}");
                _dllBytes = null;
            }
        }

        [Obsolete("For mods, use OnInitializeMelon instead. For plugins, use OnPreModsLoaded instead.")]
        public override void OnApplicationStart()
        {
            if (_dllBytes != null)
            {
                try
                {
                    InitializeDeepCore(_dllBytes);
                }
                catch (Exception ex)
                {
                    MelonLogger.Error("Failed to initialize DeepCore: " + ex.Message);
                }
            }
            else
            {
                MelonLogger.Error("DeepCore initialization failed - no DLL bytes available.");
            }
        }

        private void InitializeDeepCore(byte[] dllBytes)
        {
            MelonLogger.Msg("Initializing DeepCore...");
            // Your initialization logic here
        }
    }
}