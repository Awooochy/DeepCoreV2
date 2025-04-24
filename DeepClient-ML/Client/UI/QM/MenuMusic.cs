using System;
using System.Collections;
using System.IO;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;
using VRC.UI.Elements.Controls;

namespace DeepCore.Client.UI.QM
{
    internal class MenuMusic
    {
        public static AudioClip clip;
        public static bool isPlaying = false;
        public static bool IsLoaded = false;
        public static bool IsMenuOpen = false;
        public static MenuStateController menuStateController = GameObject.Find("Canvas_QuickMenu(Clone)").GetComponent<MenuStateController>();
        public static MenuStateController menuStateControllerMain = GameObject.Find("Canvas_MainMenu(Clone)").GetComponent<MenuStateController>();

        public static void OnUpdate()
        {
            if (IsLoaded)
            { 
            bool flag = ConfManager.ShouldMenuMusic.Value && ((menuStateController != null && menuStateController.enabled) || (menuStateController != null && menuStateControllerMain.enabled));
            if (flag)
            {
                bool flag2 = GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>() != null && !GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>().isPlaying;
                if (flag2)
                {
                    GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>().Play();
                }
            }
            else
            {
                bool flag3 = GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>() != null && GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>().isPlaying;
                if (flag3)
                {
                    GameObject.Find("Canvas_QuickMenu(Clone)/MenuMusic").GetComponent<AudioSource>().Stop();
                }
            }
        }
    }
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
                DeepConsole.LogConsole("MenuMusic", "Exception:" + ex.Message);
            }
        }
        public static IEnumerator MenuMusicInit()
        {
            if (File.Exists("DeepClient/LoadingMusic/MenuMusic.ogg"))
            { }
            else
            {
                DownloadFiles("https://nigga.rest/where/DownloadableResources/MenuMusic.ogg", "DeepClient/LoadingMusic/MenuMusic.ogg");
            }
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
                var download = new UnityWebRequest("\"https://nigga.rest/where/DownloadableResources/MenuMusic.ogg", UnityWebRequest.kHttpVerbGET);
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
        public static byte[] DownloadFiles(string downloadUrl, string savePath)
        {
            byte[] result = null;
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    result = webClient.DownloadData(downloadUrl);
                    File.WriteAllBytes(savePath, result);
                    DeepConsole.Log("AudioManager", $"Downloaded: {savePath}");
                }
                catch (Exception ex)
                {
                    DeepConsole.Log("AudioManager", $"An error occurred while downloading: {ex}");
                }
            }
            return result;
        }
    }
}