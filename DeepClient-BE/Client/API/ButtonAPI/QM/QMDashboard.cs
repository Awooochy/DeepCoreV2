using System;
using TMPro;
using UnityEngine;
using VRC.Localization;

namespace DeepClient.Client.API.ButtonAPI.QM
{
    internal class QMDashboard
    {
        public static void Setup()
        {
            Misc.VrcExtensions.SetQmDashbordPageTittle($"{ClientUtils.GetGreeting()}!");
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect").GetComponent<ScrollRectPublicIBeginDragHandlerIEventSystemHandlerIEndDragHandlerIDragHandlerIScrollHandlerSiSt_cBomiSi_aBoUI_fUnique>().enabled = true;
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners").SetActive(false);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_VRCPlus").SetActive(false);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_VRCPlusExperiment").SetActive(false);
        }
        public static void AddHeader(string ObjectName, string Text)
        {
            GameObject.Instantiate(GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickActions"), GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/").transform);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickActions(Clone)").name = ObjectName + "Header";
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/" + ObjectName + "Header/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().m_Color = Color.white;
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/" + ObjectName + "Header/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>().m_text = (Text);
        }
        public static void CreateButtonPref(string ObjectName)
        {
            GameObject.Instantiate(GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions"), GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/").transform);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions(Clone)").name = ObjectName + "BtnPref";
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/" + ObjectName + "BtnPref/Button_GoHome").SetActive(false);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/" + ObjectName + "BtnPref/Button_Respawn").SetActive(false);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/" + ObjectName + "BtnPref/Button_SelectUser").SetActive(false);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/" + ObjectName + "BtnPref/SitStandCalibrateButton").SetActive(false);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/" + ObjectName + "BtnPref/Button_Safety").SetActive(false);
        }
        public static void AddButton(string CatName,string ObjectName, string Text,string Tooltip, Action action)
        {
            GameObject.Instantiate(GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_GoHome"), GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/"+CatName+"BtnPref/").transform);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/"+CatName+ "BtnPref/Button_GoHome(Clone)").name = ObjectName + "Button";
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/"+CatName+"BtnPref/"+ObjectName+"Button/Icons").SetActive(false);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/"+CatName+"BtnPref/"+ObjectName+"Button").GetComponent<MonoBehaviourPublicLi1ObUnique>().enabled = false;
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/"+ CatName+ "BtnPref/"+ObjectName+"Button/TextLayoutParent/Text_H4").GetComponent<TextMeshProUGUI>().m_text = (Text);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/"+CatName+ "BtnPref/" +ObjectName+"Button").GetComponent<VRCButtonHandle>().onClick.AddListener((UnityEngine.Events.UnityAction)action);
            BAPIUtils.SetToolTipText("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/" + CatName + "BtnPref/" + ObjectName + "Button", Tooltip);
        }
    }
}
