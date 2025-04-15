using System;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.UI;

namespace DeepClient.Client.Misc
{
    internal class SpriteManager
    {
        public static void LoadSprite()
        {
            if (File.Exists("DeepClient/ClientIcon.png"))
            {
            }
            else
            {
                DeepConsole.Log("SpriteManager", $"File not found: {"DeepClient/ClientIcon.png"}, Downloading...");
                DownloadFiles("https://github.com/TMatheo/FileHost/blob/main/DeepClient/ClientIcon.png?raw=true", "DeepClient/ClientIcon.png");
            }
            if (File.Exists("DeepClient/LoadingBackgrund.png"))
            {
            }
            else
            {
                DeepConsole.Log("SpriteManager", $"File not found: {"DeepClient/LoadingBackgrund.png"}, Downloading...");
                DownloadFiles("https://github.com/TMatheo/FileHost/blob/main/DeepClient/LoadingBackgrund.png?raw=true", "DeepClient/LoadingBackgrund.png");
            }
            if (File.Exists("DeepClient/MMBG.png"))
            {
            }
            else
            {
                DeepConsole.Log("SpriteManager", $"File not found: {"DeepClient/MMBG.png"}, Downloading...");
                DownloadFiles("https://github.com/TMatheo/FileHost/blob/main/DeepClient/MMBG.png?raw=true", "DeepClient/MMBG.png");
            }
            if (File.Exists("DeepClient/QMBG.png"))
            {
            }
            else
            {
                DeepConsole.Log("SpriteManager", $"File not found: {"DeepClient/QMBG.png"}, Downloading...");
                DownloadFiles("https://github.com/TMatheo/FileHost/blob/main/DeepClient/QMBG.png?raw=true", "DeepClient/QMBG.png");
            }
        }
        public static byte[] DownloadFiles(string downloadUrl, string savePath)
        {
            byte[] result = null;
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    result = webClient.DownloadData(downloadUrl);
                    File.WriteAllBytes(savePath, result);
                    DeepConsole.Log("SpriteManager", $"Downloaded: {savePath}");
                }
                catch (Exception ex)
                {
                    DeepConsole.Log("SpriteManager", $"An error occurred while downloading: {ex}");
                }
            }
            return result;
        }
    }
}
