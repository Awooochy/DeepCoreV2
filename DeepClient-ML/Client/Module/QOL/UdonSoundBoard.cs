using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using VRC.Udon;

namespace DeepCore.Client.Module.QOL
{
    internal class UdonSoundBoard
    {
        public static bool IsLoaded = false;
        public static AudioClip clip;
        public static readonly HashSet<string> AllowedSoundEvent = new HashSet<string>
        {
            "DCCapybara",
            "DC"
        };
        public static void Setup()
        {
            if (false)
            {
                if (!Directory.Exists("DeepClient/SoundBoard"))
                {
                    Directory.CreateDirectory("DeepClient/SoundBoard");
                }
                if (!File.Exists("DeepClient/SoundBoard/Capybara.ogg"))
                {
                    Misc.SpriteManager.DownloadFiles("https://github.com/TMatheo/FileHost/raw/refs/heads/main/DeepClient/SoundBoard/Capybara.ogg", "DeepClient/SoundBoard/Capybara.ogg");
                }
                SetupObj();
            }
        }
        public static void SetupObj()
        {
            new GameObject("SoundBoard").transform.parent = GameObject.Find("Canvas_QuickMenu(Clone)/").transform;
            GameObject.Find("Canvas_QuickMenu(Clone)/SoundBoard").AddComponent<UdonBehaviour>();
            GameObject.Find("Canvas_QuickMenu(Clone)/SoundBoard").AddComponent<AudioSource>();
            IsLoaded = true;
        }
        public static void SendSound(string EventThing)
        {
            if (IsLoaded)
            {
                GameObject.Find("Canvas_QuickMenu(Clone)/SoundBoard").GetComponent<UdonBehaviour>().SendCustomEvent("OnCustomEvent");
            }
        }
        public static void OnCustomEvent(string EventThing)
        {
            if (IsLoaded)
            {
                if (AllowedSoundEvent.Contains(EventThing))
                {
                    DeepConsole.Log("SoundBoard", $"Received A");
                }
                if (AllowedSoundEvent.Contains(EventThing))
                {
                    DeepConsole.Log("SoundBoard", $"Received D");
                }
            }
        }
        public static void PlaySound(string sound)
        {
            string path = Path.Combine(Directory.CreateDirectory("DeepClient/SoundBoard").FullName, $"{sound}.ogg");
            UnityWebRequest localfile = UnityWebRequest.Get("file://" + path);
            clip = WebRequestWWW.InternalCreateAudioClipUsingDH(localfile.downloadHandler, localfile.url, false, false, 0);
            AudioSource musicObj = GameObject.Find("LoadingBackground_TealGradient_Music/LoadingSound").GetComponent<AudioSource>();
            musicObj.clip = clip;
            musicObj.volume = 100;
            musicObj.Play();
        }
    }
}
