using System;
using System.Collections;
using System.IO;
using System.Linq;
using MelonLoader;
using UnityEngine;

namespace DeepCore.Client.Module.QOL
{
    internal class LoadingScreenPictures
    {
        public static GameObject mainFrame;
        public static GameObject cube;
        public static Texture lastTexture;
        public static Renderer screenRender, pic;
        public static string folder_dir;
        public static bool initUI = false;
        public static bool enabled = true;
        public static bool IsLoaded = true;
        public static Vector3 originalSize;
        public static void OnApplicationStart()
        {
            MelonCoroutines.Start(UiManagerInitializer());
            string default_dir = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures) + @"\VRChat";
            MelonPreferences.CreateCategory("LoadingScreenPictures", "Loading Screen Pictures");
            MelonPreferences.CreateEntry("LoadingScreenPictures", "directory", default_dir, "Folder to get pictures from");
            MelonPreferences.CreateEntry("LoadingScreenPictures", "enabled", true, "Enable");
            folder_dir = MelonPreferences.GetEntryValue<string>("LoadingScreenPictures", "directory");
            enabled = MelonPreferences.GetEntryValue<bool>("LoadingScreenPictures", "enabled");
            if (default_dir != folder_dir && !Directory.Exists(folder_dir))
            {
                folder_dir = default_dir;
                DeepConsole.Log("LoadingScreenPictures", "Couldn't find configured directory, using default directory.");
            }
        }
        public static IEnumerator UiManagerInitializer()
        {
            while (GameObject.Find("MenuContent") == null) yield return null;
            setup();
            initUI = true;
        }
        public static void OnPreferencesSaved()
        {
            enabled = MelonPreferences.GetEntryValue<bool>("LoadingScreenPictures", "enabled");
            if (enabled) setup();
            else disable();
        }

        public static float wait = 0.0f;
        public static bool hidden = false;

        public static void OnUpdate()
        {
            if (!enabled) return;

            if (Time.time > wait)
            {
                wait += 5f;
                if (hidden)
                {
                    hidden = false;
                    setup();
                }
            }

            if (lastTexture == null) return;
            if (lastTexture == screenRender.material.mainTexture) return;
            lastTexture = screenRender.material.mainTexture;
            changePic();
        }
        public static void OnSceneWasInitialized(int buildIndex, string sceneName)
        {
            switch (buildIndex)
            {
                case 1:
                case 2:
                    break;
                default:
                    if (initUI && lastTexture == null)
                        setup();
                    break;
            }
        }
        public static void changePic()
        {
            Texture2D texture = new Texture2D(2, 2);
            ImageConversion.LoadImage(texture, File.ReadAllBytes(randImage()));
            pic.material.mainTexture = texture;
            if (pic.material.mainTexture.height > pic.material.mainTexture.width)
            {
                cube.transform.localScale = new Vector3(0.099f, 1, 0.175f);
                mainFrame.transform.localScale = new Vector3(10.80f, 19.20f, 1);
            }
            else
            {
                cube.transform.localScale = new Vector3(0.175f, 1, 0.099f);
                mainFrame.transform.localScale = new Vector3(19.20f, 10.80f, 1);
            }
        }
        public static void disable()
        {
            DeepConsole.Log("LoadingScreenPictures", "Disabled.");
            if (mainFrame) mainFrame.transform.localScale = originalSize;
            if (screenRender) screenRender.enabled = true;
            if (cube) GameObject.Destroy(cube);
            lastTexture = null;
        }
        public static void setup()
        {
            if (!enabled || lastTexture != null) return;
            mainFrame = GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel/InfoPanel_Template_ANIM/SCREEN/mainFrame");
            originalSize = mainFrame.transform.localScale;
            GameObject screen = GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel/InfoPanel_Template_ANIM/SCREEN/mainScreen");
            String imageLink = randImage();
            if (imageLink == null)
            {
                DeepConsole.Log("LoadingScreenPictures", $"No screenshots found in:{folder_dir}");
                return;
            }
            GameObject parentScreen = GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel/InfoPanel_Template_ANIM/SCREEN");
            screenRender = screen.GetComponent<Renderer>();
            lastTexture = screenRender.material.mainTexture;
            cube = GameObject.CreatePrimitive(PrimitiveType.Plane);
            cube.transform.SetParent(parentScreen.transform);
            cube.transform.rotation = screen.transform.rotation;
            cube.transform.localPosition = new Vector3(0, 0, -0.19f);
            cube.GetComponent<Collider>().enabled = false;
            cube.layer = LayerMask.NameToLayer("InternalUI");
            Texture2D texture = new Texture2D(2, 2);
            ImageConversion.LoadImage(texture, File.ReadAllBytes(imageLink));
            pic = cube.GetComponent<Renderer>();
            pic.material.mainTexture = texture;
            screenRender.enabled = false;
            if (pic.material.mainTexture.height > pic.material.mainTexture.width)
            {
                cube.transform.localScale = new Vector3(0.099f, 1, 0.175f);
                mainFrame.transform.localScale = new Vector3(10.80f, 19.20f, 1);
            }
            else
            {
                cube.transform.localScale = new Vector3(0.175f, 1, 0.099f);
                mainFrame.transform.localScale = new Vector3(19.20f, 10.80f, 1);
            }
            GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel/InfoPanel_Template_ANIM/ICON").active = false;
            GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingInfoPanel/InfoPanel_Template_ANIM/TITLE").active = false;
            DeepConsole.Log("LoadingScreenPictures", "Setup Game Objects.");

        }
        public static String randImage()
        {
            if (!Directory.Exists(folder_dir)) return null;
            string[] pics = Directory.GetFiles(folder_dir, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".png") || s.EndsWith(".jpeg")).ToArray();
            if (pics.Length == 0) return null;
            int randPic = new Il2CppSystem.Random().Next(1, pics.Length);
            return pics[randPic].ToString();
        }
    }
}