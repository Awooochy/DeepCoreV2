using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections;
using System.IO;

namespace DeepClient.Client.ClientMenu
{
    internal class MenuMusic
    {
        public static AudioClip clip;
        public static bool isPlaying = false;
        public static bool IsLoaded = false;
        public static bool IsMenuOpen = false;

        public static void State(bool s)
        {
            try
            {
                if (IsLoaded)
                {
                    if (s)
                    {
                        GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>().Play();
                        IsMenuOpen = true;
                    }
                    else
                    {
                        GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>().Stop();
                        IsMenuOpen = false;
                    }
                }
            }
            catch (Exception ex)
            {
                DeepConsole.Log("MenuMusic", "Exception:" + ex.Message);
            }
        }
        public static IEnumerator MenuMusicInit()
        {
            while (GameObject.Find("Canvas_QuickMenu(Clone)") == null)
            {
                yield return null;
            }
            new GameObject("MenuMusic").transform.parent = GameObject.Find("Canvas_QuickMenu(Clone)/").transform;
            GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").AddComponent<AudioSource>();
            GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>().loop = true;
            string path = Path.Combine(Directory.CreateDirectory("DeepClient/LoadingMusic").FullName, "MenuMusic.ogg");
            if (!File.Exists(path))
            {
                var download = new UnityWebRequest("https://github.com/TMatheo/FileHost/raw/refs/heads/main/DeepClient/MenuMusic.ogg", UnityWebRequest.kHttpVerbGET);
                download.downloadHandler = new DownloadHandlerFile(path);
                yield return download.SendWebRequest();
            }
            UnityWebRequest localfile = UnityWebRequest.Get("file://" + path);
            yield return localfile.SendWebRequest();
            clip = WebRequestWWW.InternalCreateAudioClipUsingDH(localfile.downloadHandler, localfile.url, false, false, 0);
            AudioSource musicObj = GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>();
            while (GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic") == null) yield return new WaitForEndOfFrame();
            musicObj.clip = clip;
            musicObj.volume = 100;
            musicObj.Stop();
            IsLoaded = true;
            yield break;
        }
    }
}