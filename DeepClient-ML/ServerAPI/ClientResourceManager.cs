using System;
using System.IO;
using System.Net;
using DeepCore.Client;
using UnityEngine;

namespace DeepCore.ServerAPI.ClientResourceManager
{
    internal static class ClientResourceManager
    {
        // Base directories
        private const string ClientDirectory = "DeepClient";
        private const string LoadingMusicDir = "DeepClient/LoadingMusic";
        private const string AssetBundlesDir = "DeepClient/AssetBundles";
        private const string DependenciesDir = "DeepClient/Dependencies";
        private const string SoundBoardDir = "DeepClient/SoundBoard";
        
        private const string ResourceBaseUrl = "https://nigga.rest/where/DownloadableResources/";
        
        // File structure definition
        private static readonly (string FileName, string TargetDirectory)[] RequiredResources = 
        {
            // Root directory files
            ("ClientIcon.png", ClientDirectory),
            ("LoadingBackgrund.png", ClientDirectory),
            ("MMBG.png", ClientDirectory),
            ("QMBG.png", ClientDirectory),
            
            // Loading music files
            ("LoginScreenMusic.ogg", LoadingMusicDir),
            ("MenuMusic.ogg", LoadingMusicDir),
            
            // Asset bundles
            ("loadingscreen", AssetBundlesDir),
            
            // Dependencies
            ("discord-rpc.dll", DependenciesDir),
            
            //Soudnboard audio files
            ("Capybara.ogg", SoundBoardDir),
            ("Nega.ogg", SoundBoardDir)
        };

        public static void EnsureAllResourcesExist()
        {
            EnsureDirectoryStructure();
            
            foreach (var resource in RequiredResources)
            {
                string filePath = Path.Combine(resource.TargetDirectory, resource.FileName);
                if (!File.Exists(filePath))
                {
                    DownloadFile(resource.FileName, resource.TargetDirectory);
                }
            }
        }

        public static bool TryGetResourcePath(string fileName, string subDirectory, out string fullPath)
        {
            string targetDir = string.IsNullOrEmpty(subDirectory) ? 
                ClientDirectory : 
                Path.Combine(ClientDirectory, subDirectory);
            
            fullPath = Path.Combine(targetDir, fileName);
            return File.Exists(fullPath);
        }

        private static void EnsureDirectoryStructure()
        {
            CreateDirectoryIfNotExists(ClientDirectory);
            CreateDirectoryIfNotExists(LoadingMusicDir);
            CreateDirectoryIfNotExists(AssetBundlesDir);
            CreateDirectoryIfNotExists(DependenciesDir);
        }

        private static void CreateDirectoryIfNotExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                DeepConsole.Log("ClientResourceManager", $"Created directory: {path}");
            }
        }

        private static void DownloadFile(string fileName, string targetDirectory)
        {
            string filePath = Path.Combine(targetDirectory, fileName);
            string downloadUrl = $"{ResourceBaseUrl}{fileName}";
            
            DeepConsole.Log("ClientResourceManager", $"File not found: {filePath}, Downloading...");
            
            try
            {
                byte[] fileData = DownloadFileData(downloadUrl);
                File.WriteAllBytes(filePath, fileData);
                DeepConsole.Log("ClientResourceManager", $"Successfully downloaded: {filePath}");
            }
            catch (Exception ex)
            {
                DeepConsole.Log("ClientResourceManager", 
                    $"Failed to download {fileName} to {targetDirectory}: {ex.Message}");
                
                // For DLLs, the error might be critical
                if (fileName.EndsWith(".dll"))
                {
                    DeepConsole.Log("ClientResourceManager", 
                        "CRITICAL: Failed to download a dependency DLL. Some features may not work.");
                }
            }
        }

        private static byte[] DownloadFileData(string url)
        {
            using (WebClient webClient = new WebClient())
            {
                return webClient.DownloadData(url);
            }
        }
    }
}