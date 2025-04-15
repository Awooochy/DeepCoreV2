using System;
using System.Collections;
using System.IO;
using System.Net;
using DeepCore.Client.Misc;
using MelonLoader;
using UnityEngine;

namespace DeepCore.Client.Coroutine
{
    internal class LoadingParticules
    {
        public static GameObject partsystem;
        public static IEnumerator loadparticles()
        {
            if (File.Exists("DeepClient\\AssetBundles\\loadingscreen"))
            { }
            else
            {
                DownloadFiles("https://github.com/TMatheo/FileHost/raw/refs/heads/main/DeepClient/loadingscreen", "DeepClient\\AssetBundles\\loadingscreen");
            }
            byte[] loadingparticles = File.ReadAllBytes($"{MelonUtils.GameDirectory}\\DeepClient\\AssetBundles\\loadingscreen");
            var myLoadedAssetBundle = AssetBundle.LoadFromMemory(loadingparticles);
            if (myLoadedAssetBundle == null)
            {
                DeepConsole.Log("LS","Failed to load AssetBundle!");
                yield break;
            }
            partsystem = myLoadedAssetBundle.LoadAsset<GameObject>("ParticleLoader");
            var gmj = GameObject.Instantiate(partsystem, GameObject.Find("MenuContent/Popups/LoadingPopup").transform);
            gmj.transform.localPosition = new Vector3(0, 0, 8000);
            gmj.transform.Find("finished").gameObject.transform.localPosition = new Vector3(0, 0, 10000);
            gmj.transform.Find("finished/Other").gameObject.transform.localPosition = new Vector3(0, 0, 3000);
            gmj.transform.Find("middle").gameObject.transform.localPosition = new Vector3(-50, 0f, 10000);
            gmj.transform.Find("cirlce mid").gameObject.transform.localPosition = new Vector3(-673.8608f, 0, 4000);
            gmj.transform.Find("spawn").gameObject.transform.localPosition = new Vector3(800, 0, -8500f);
            foreach (var gmjs in gmj.GetComponentsInChildren<ParticleSystem>(true))
            {
                gmjs.startColor = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
                gmjs.trails.colorOverTrail = new Color(UIColorManager.HRed, UIColorManager.HGreen, UIColorManager.HBlue);
            }
            GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements").gameObject.SetActive(false);
            while (GameObject.Find("DesktopUImanager") == null)
                yield return null;
            var toload = myLoadedAssetBundle.LoadAsset<GameObject>("Holder");
            myLoadedAssetBundle.Unload(false);
            var gmjsa = GameObject.Instantiate(toload, GameObject.Find("DesktopUImanager").transform);
            gmjsa.transform.localPosition = new Vector3(0, 360.621f, 700);
            gmjsa.transform.localRotation = new Quaternion(0, 0, 0, 0);
            gmjsa.transform.localScale = new Vector3(1, 1, 1);
            var p1 = gmjsa.transform.Find("Particle System").transform;
            p1.localScale = new Vector3(0.08f, 0.08f, 0.08f);
            p1.localPosition = new Vector3(0, 64.16f, 7.2f);
            var p2 = gmjsa.transform.Find("Particle System (1)").transform;
            p2.localScale = new Vector3(0.06f, 0.06f, 0.06f);
            p2.localPosition = new Vector3(-30.78f, -321.5403f, 8.54f);
            yield return null;
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
                    DeepConsole.Log("LoadingManager", $"Downloaded: {savePath}");
                }
                catch (Exception ex)
                {
                    DeepConsole.Log("LoadingManager", $"An error occurred while downloading: {ex}");
                }
            }
            return result;
        }
    }
}
