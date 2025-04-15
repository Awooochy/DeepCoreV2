using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using MelonLoader;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace DeepCore.Client.Module.QOL
{
    internal class OldLoadingScreenMod
    {
        public static GameObject loadScreenPrefab;
        public static GameObject loginPrefab;
        public static AssetBundle assets;
        private static readonly string assetBundlePath = "DeepClient/loading.assetbundle";

        public static void OnApplicationStart()
        {
            MelonCoroutines.Start(WaitForUi());
            MelonCoroutines.Start(GetAssembly());
        }

        public static IEnumerator GetAssembly()
        {
            Assembly assemblyCSharp = null;
            while (true)
            {
                assemblyCSharp = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(assembly => assembly.GetName().Name == "Assembly-CSharp");
                if (assemblyCSharp == null)
                    yield return null;
                else
                    break;
            }
        }

        public static IEnumerator WaitForUi()
        {
            while (GameObject.Find("MenuContent") == null)
            {
                yield return null;
            }
            
            DeepConsole.Log("BLS", "Loading AssetBundle from path...");
            if (File.Exists(assetBundlePath))
            {
                byte[] assetData = File.ReadAllBytes(assetBundlePath);
                assets = AssetBundle.LoadFromMemory_Internal(assetData, 0);
                assets.hideFlags |= HideFlags.DontUnloadUnusedAsset;

                loadScreenPrefab = assets.LoadAsset_Internal("Assets/Bundle/LoadingBackground.prefab", Il2CppType.Of<GameObject>()).Cast<GameObject>();
                loadScreenPrefab.hideFlags |= HideFlags.DontUnloadUnusedAsset;

                loginPrefab = assets.LoadAsset_Internal("Assets/Bundle/Login.prefab", Il2CppType.Of<GameObject>()).Cast<GameObject>();
                loginPrefab.hideFlags |= HideFlags.DontUnloadUnusedAsset;
                
                CreateGameObjects();
            }
            else
            {
                DeepConsole.Log("BLS", "AssetBundle file not found at: " + assetBundlePath);
            }
        }

        public static void CreateGameObjects()
        {
            DeepConsole.Log("BLS", "Finding original GameObjects");
            var SkyCube = GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/SkyCube_Baked");
            var bubbles = GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_FX_ParticleBubbles");
            var originalLoadingAudio = GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingSound");

            DeepConsole.Log("BLS", "Creating new GameObjects");
            loadScreenPrefab = CreateGameObject(loadScreenPrefab, new Vector3(400, 400, 400), "MenuContent/Popups/", "LoadingPopup");
            loginPrefab = CreateGameObject(loginPrefab, new Vector3(0.5f, 0.5f, 0.5f), "LoadingBackground_TealGradient_Music", "");

            DeepConsole.Log("BLS", "Disabling original GameObjects");
            SkyCube.active = false;
            bubbles.active = false;
            originalLoadingAudio.active = false;
            OnPreferencesSaved();
        }
        public static void OnPreferencesSaved()
        {
            DeepConsole.Log("BLS", "Applying Preferences");
            loadScreenPrefab = GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingBackground(Clone)/");
            var music = loadScreenPrefab.transform.Find("MenuMusic");
            var spaceSound = loadScreenPrefab.transform.Find("SpaceSound");
            var cube = loadScreenPrefab.transform.Find("SkyCube");
            var particles = loadScreenPrefab.transform.Find("Stars");
            var warpTunnel = loadScreenPrefab.transform.Find("Tunnel");
            var logo = loadScreenPrefab.transform.Find("VRCLogo");
            var InfoPanel = GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel");
            var originalLoadingAudio = GameObject.Find("MenuContent/Popups/LoadingPopup/LoadingSound");
            var aprfools = loadScreenPrefab.transform.Find("meme");
            var originalStartScreenSkyCube = GameObject.Find("LoadingBackground_TealGradient_Music/SkyCube_Baked");
            originalStartScreenSkyCube.active = false;
            if (ConfManager.BLSModSounds.Value)
            {
                music.gameObject.SetActive(true);
                spaceSound.gameObject.SetActive(true);
                originalLoadingAudio.SetActive(false);
            }
            else
            {
                music.gameObject.SetActive(false);
                spaceSound.gameObject.SetActive(false);
                originalLoadingAudio.SetActive(true);
            }

            if (ConfManager.BLSWarpTunnel.Value)
            {
                warpTunnel.gameObject.SetActive(true);
            }
            else
            {
                warpTunnel.gameObject.SetActive(false);
            }
            if (ConfManager.BLSVrcLogo.Value)
            {
                logo.gameObject.SetActive(true);
            }
            else
            {
                logo.gameObject.SetActive(false);
            }

            if (ConfManager.BLShowLoadingMessages.Value)
            {
                InfoPanel.SetActive(true);
            }
            else
            {
                InfoPanel.SetActive(false);
            }

            if (DateTime.Today.Month == 4 && DateTime.Now.Day == 1)
            {
                logo.gameObject.SetActive(false);
                aprfools.gameObject.SetActive(true);
            }
        }
        public static GameObject CreateGameObject(GameObject obj, Vector3 scale, string rootDest, string parent)
        {
            DeepConsole.Log("BLS", "Creating " + obj.name);
            var UIRoot = GameObject.Find(rootDest);
            var requestedParent = UIRoot.transform.Find(parent);
            var newObject = GameObject.Instantiate(obj, requestedParent, false).Cast<GameObject>();
            newObject.transform.parent = requestedParent;
            newObject.transform.localScale = scale;
            return newObject;
        }
    }
}
