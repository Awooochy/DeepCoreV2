using System;
using System.IO;

namespace DeepCore.Client.Module.QOL
{
    internal class CacheCleanRam
    {
        public static void Clean()
        {
            string localLowPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData).Replace("Local", "LocalLow");
            string vrchatCachePath = Path.Combine(localLowPath, "VRChat", "VRChat", "Cache-WindowsPlayer");

            if (Directory.Exists(vrchatCachePath))
            {
                try
                {
                    Directory.Delete(vrchatCachePath, true);
                    DeepConsole.Log("Cleaner", "VRChat cache cleaned successfully.");
                }
                catch (Exception ex)
                {
                    DeepConsole.Log("Cleaner", "Error cleaning VRChat cache: " + ex.Message);
                }
            }
            else
            {
                DeepConsole.Log("Cleaner", "VRChat cache directory not found. WTF ???");
            }
        }
    }
}
