using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace DeepClient.Client.Coroutines
{
    internal class CustomMenuBG
    {
        public static bool IsLoaded = false;
        public static IEnumerator Init()
        {
            while (GameObject.Find("MenuContent/Backdrop/Backdrop/Background") == null)
            {
                yield return null;
            }
            GameObject.Find("MenuContent/Backdrop/Backdrop/Background").GetComponent<Image>().overrideSprite = Misc.Resources.LoadSprite("MMBG.png");
            GameObject.Find("MenuContent/Backdrop/Backdrop/Background").GetComponent<Image>().color = Color.white;
            while (GameObject.Find("Canvas_QuickMenu(Clone)") == null)
            {
                yield return null;
            }
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/BackgroundLayer01").GetComponent<ImageEx>().overrideSprite = Misc.Resources.LoadSprite("QMBG.png");
            while (GameObject.Find("Canvas_MainMenu(Clone)") == null)
            {
                yield return null;
            }
            GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/BackgroundLayer01").GetComponent<ImageEx>().overrideSprite = Misc.Resources.LoadSprite("MMBG.png");
            IsLoaded = true;
        }
        public static void ApplyColors()
        {
            if (IsLoaded)
            {
                GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/BackgroundLayer01").GetComponent<ImageEx>().color = Color.white;
                GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/BackgroundLayer01").GetComponent<ImageEx>().m_Color = Color.white;
                GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/BackgroundLayer01").GetComponent<ImageEx>().color = Color.white;
                GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/BackgroundLayer01").GetComponent<ImageEx>().m_Color = Color.white;
                IsLoaded = false;
            }
        }
    }
}