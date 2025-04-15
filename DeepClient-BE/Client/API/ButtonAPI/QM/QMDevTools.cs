using System;
using TMPro;
using UnityEngine;

namespace DeepClient.Client.API.ButtonAPI.QM
{
    internal class QMDevTools
    {
        public static void Setup(string ClientName)
        {
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_DevTools").SetActive(true);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_DevTools/Header_DevTools/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().m_text = (ClientName);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_DevTools").SetActive(true);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_DevTools/Icon").GetComponent<ImageEx>().overrideSprite = Misc.Resources.LoadSprite("ClientIcon.png");
            BAPIUtils.SetToolTipText("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_DevTools", "Open the DeepClient menu.");
            GameObject.DestroyImmediate(GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_DevTools/Scrollrect/Viewport/VerticalLayoutGroup/Buttons/Button_WarpAllToHub"));
            GameObject.DestroyImmediate(GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_DevTools/Scrollrect/Viewport/VerticalLayoutGroup/Buttons/Button_WarpAllToNewInstance"));
            GameObject.DestroyImmediate(GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_DevTools/Scrollrect/Viewport/VerticalLayoutGroup/Buttons/Button_Invisible"));
            GameObject.DestroyImmediate(GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_DevTools/Scrollrect/Viewport/VerticalLayoutGroup/Buttons/Button_Tag"));
        }
        public static void AddButton(string ObjectName, string Text, string Tooltip, Action action)
        {
            GameObject.Instantiate(GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_GoHome"), GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_DevTools/Scrollrect/Viewport/VerticalLayoutGroup/Buttons/").transform);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_DevTools/Scrollrect/Viewport/VerticalLayoutGroup/Buttons/Button_GoHome(Clone)").name = ObjectName + "Button";
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_DevTools/Scrollrect/Viewport/VerticalLayoutGroup/Buttons/" + ObjectName + "Button/Icons").SetActive(false);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_DevTools/Scrollrect/Viewport/VerticalLayoutGroup/Buttons/" + ObjectName + "Button").GetComponent<MonoBehaviourPublicLi1ObUnique>().enabled = false;
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_DevTools/Scrollrect/Viewport/VerticalLayoutGroup/Buttons/" + ObjectName + "Button/TextLayoutParent/Text_H4").GetComponent<TextMeshProUGUI>().m_text = (Text);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_DevTools/Scrollrect/Viewport/VerticalLayoutGroup/Buttons/" + ObjectName + "Button").GetComponent<VRCButtonHandle>().onClick.AddListener((UnityEngine.Events.UnityAction)action);
            BAPIUtils.SetToolTipText("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_DevTools/Scrollrect/Viewport/VerticalLayoutGroup/Buttons/" + ObjectName + "Button",Tooltip);
        }
    }
}
