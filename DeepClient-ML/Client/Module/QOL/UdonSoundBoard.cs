using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using VRC.Udon;
using DeepCore.ServerAPI.ClientResourceManager;

namespace DeepCore.Client.Module.QOL
{
    internal class UdonSoundBoard
    {
        public static bool IsLoaded = false;
        public static AudioClip clip;
        private const string SoundBoardDir = "DeepClient/SoundBoard";
        
        public static readonly HashSet<string> AllowedSoundEvents = new HashSet<string>
        {
            "DCCapybara",
            "DC"
        };

        public static void Setup()
        {
            // Ensure all resources exist (including soundboard files)
            ClientResourceManager.EnsureAllResourcesExist();
            
            SetupObj();
        }

        public static void SetupObj()
        {
            if (GameObject.Find("Canvas_QuickMenu(Clone)/SoundBoard") == null)
            {
                var soundBoardObj = new GameObject("SoundBoard");
                soundBoardObj.transform.parent = GameObject.Find("Canvas_QuickMenu(Clone)/").transform;
                soundBoardObj.AddComponent<UdonBehaviour>();
                soundBoardObj.AddComponent<AudioSource>();
                IsLoaded = true;
            }
        }

        public static void SendSound(string eventName)
        {
            if (IsLoaded && AllowedSoundEvents.Contains(eventName))
            {
                var soundBoard = GameObject.Find("Canvas_QuickMenu(Clone)/SoundBoard");
                if (soundBoard != null)
                {
                    soundBoard.GetComponent<UdonBehaviour>().SendCustomEvent("OnCustomEvent");
                }
            }
        }

        public static void OnCustomEvent(string eventName)
        {
            if (IsLoaded && AllowedSoundEvents.Contains(eventName))
            {
                DeepConsole.Log("SoundBoard", $"Received sound event: {eventName}");
                PlaySound(eventName.Replace("DC", "")); // Remove "DC" prefix for filename
            }
        }

        public static void PlaySound(string soundName)
        {
            if (ClientResourceManager.TryGetResourcePath($"{soundName}.ogg", "SoundBoard", out string path))
            {
                UnityWebRequest localfile = UnityWebRequest.Get("file://" + path);
                clip = WebRequestWWW.InternalCreateAudioClipUsingDH(
                    localfile.downloadHandler, 
                    localfile.url, 
                    false, 
                    false, 
                    0
                );
                
                var musicObj = GameObject.Find("LoadingBackground_TealGradient_Music/LoadingSound")?.GetComponent<AudioSource>();
                if (musicObj != null)
                {
                    musicObj.clip = clip;
                    musicObj.volume = 1.0f; // Changed from 100 to 1.0f (range is 0-1)
                    musicObj.Play();
                }
            }
            else
            {
                DeepConsole.Log("SoundBoard", $"Sound file not found: {soundName}.ogg");
            }
        }
    }
}