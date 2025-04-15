using System;
using TMPro;
using UnityEngine;

namespace DeepClient.Client.API.ButtonAPI.QM
{
    internal class QMSelectedUserPage
    {
        public static void AddHeader(string ObjectName, string Text)
        {
            GameObject.Instantiate(GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickActions"), GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/").transform);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Header_QuickActions(Clone)").name = ObjectName + "Header";
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/" + ObjectName + "Header/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUIPublicLo_l1LaLo_cStLoUnique>().color = Color.white;
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/" + ObjectName + "Header/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUIPublicLo_l1LaLo_cStLoUnique>().text = (Text);
        }
        public static void CreateButtonPref(string ObjectName)
        {
            GameObject.Instantiate(GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions"), GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/").transform);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions(Clone)").name = ObjectName + "BtnPref";
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/" + ObjectName + "BtnPref/Button_GoHome").SetActive(false);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/" + ObjectName + "BtnPref/Button_Respawn").SetActive(false);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/" + ObjectName + "BtnPref/Button_SelectUser").SetActive(false);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/" + ObjectName + "BtnPref/SitStandCalibrateButton").SetActive(false);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/" + ObjectName + "BtnPref/Button_Safety").SetActive(false);
        }
        public static void AddButton(string CatName, string ObjectName, string Text, bool RemoveIcon, Action action)
        {
            GameObject.Instantiate(GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_QuickActions/Button_GoHome"), GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/" + CatName + "BtnPref/").transform);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/" + CatName + "BtnPref/Button_GoHome(Clone)").name = ObjectName + "Button";
            if (RemoveIcon)
            {
                GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/" + CatName + "BtnPref/" + ObjectName + "Button/Icons").SetActive(false);
            }
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/" + CatName + "BtnPref/" + ObjectName + "Button").GetComponent<MonoBehaviourPublicLi1ObUnique>().enabled = false;
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/" + CatName + "BtnPref/" + ObjectName + "Button/TextLayoutParent/Text_H4").GetComponent<TextMeshProUGUIPublicLo_l1LaLo_cStLoUnique>().SetText(Text);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/" + CatName + "BtnPref/" + ObjectName + "Button/TextLayoutParent/Text_H4").GetComponent<TextMeshProUGUIPublicLo_l1LaLo_cStLoUnique>().text = (Text);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/" + CatName + "BtnPref/" + ObjectName + "Button").GetComponent<VRCButtonHandle>().onClick.AddListener((UnityEngine.Events.UnityAction)action);
        }
    }
}
