using System;
using UnityEngine;
using DeepCore.ServerAPI.ClientResourceManager;

namespace DeepCore.Client.Misc
{
    internal static class SpriteManager
    {
        public static Sprite ClientIcon { get; private set; }
        public static Sprite LoadingBackground { get; private set; }
        public static Sprite MainMenuBackground { get; private set; }
        public static Sprite QuickMenuBackground { get; private set; }

        public static void LoadAllSprites()
        {
            try
            {
                // Ensure all resources are available
                ClientResourceManager.EnsureAllResourcesExist();
                
                // Load each sprite with proper error handling
                ClientIcon = LoadSprite("ClientIcon.png");
                LoadingBackground = LoadSprite("LoadingBackgrund.png"); // Note: Typo consistent with file name
                MainMenuBackground = LoadSprite("MMBG.png");
                QuickMenuBackground = LoadSprite("QMBG.png");

                // Verify all sprites loaded
                if (ClientIcon == null || LoadingBackground == null)
                {
                    DeepConsole.Log("SpriteManager", "Failed to load one or more sprites");
                }
            }
            catch (Exception ex)
            {
                DeepConsole.Log("SpriteManager", $"Fatal error during sprite loading: {ex.Message}");
            }
        }

        private static Sprite LoadSprite(string fileName)
        {
            try
            {
                if (ClientResourceManager.TryGetResourcePath(fileName, "", out string filePath))
                {
                    var sprite = ReMod.Core.Managers.ResourceManager.LoadSpriteFromDisk(filePath);
                    if (sprite == null)
                    {
                        DeepConsole.Log("SpriteManager", $"Loaded null sprite from: {filePath}");
                    }
                    return sprite;
                }

                DeepConsole.Log("SpriteManager", $"Sprite file not found: {fileName}");
                return null;
            }
            catch (Exception ex)
            {
                DeepConsole.Log("SpriteManager", 
                    $"Error loading sprite {fileName}: {ex.Message}");
                return null;
            }
        }

        public static void ReloadSprites()
        {
            DeepConsole.Log("SpriteManager", "Reloading all sprites...");
            LoadAllSprites();
        }
    }
}