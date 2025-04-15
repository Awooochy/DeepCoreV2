using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

namespace DeepCore.Client.Coroutine
{
    internal class CustomVRLoadingOverlay
    {
        public static IEnumerator Init()
        {
            float timeout = 2f;
            float elapsedTime = 0f;

            while (GameObject.Find("TrackingVolume/VRLoadingOverlay/FlatLoadingOverlay(Clone)") == null)
            {
                if (elapsedTime >= timeout)
                {
                    Debug.LogWarning("GameObject not found! Stopping coroutine.");
                    DeepConsole.Log("Startup", "Can't find FlatLoadingOverlay(Clone), User may being in VR.");
                    yield break;
                }
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            GameObject overlay = GameObject.Find("TrackingVolume/VRLoadingOverlay/FlatLoadingOverlay(Clone)/Container/Canvas/Background");
            if (overlay == null)
            {
                DeepConsole.Log("Startup", "Can't find Overlay background.");
                yield break;
            }

            Image overlayImage = overlay.GetComponent<Image>();
            if (overlayImage == null)
            {
                DeepConsole.Log("Startup", "Image component missing on overlay background !");
                yield break;
            }

            string filePath = "DeepClient/LoadingBackgrund.png";
            if (File.Exists(filePath))
            {
                overlayImage.color = Color.white;
                overlayImage.overrideSprite = ReMod.Core.Managers.ResourceManager.LoadSpriteFromDisk(filePath);
            }
            else
            {
                Misc.SpriteManager.DownloadFiles(
                    "https://github.com/TMatheo/FileHost/blob/main/DeepClient/LoadingBackgrund.png?raw=true",
                    "DeepClient/DeepClient/LoadingBackgrund.png"
                );
            }
        }
    }
}
