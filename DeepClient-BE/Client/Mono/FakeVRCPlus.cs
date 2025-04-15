using System.Collections;
using DeepClient.Client.Misc.DeepClient.Client.Misc;
using UnityEngine;

namespace DeepClient.Client.Mono
{
    public class FakeVRCPlus : MonoBehaviour
    {
        public void VRCPBEventButton()
        {
            GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_VRChat+/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Page_MM_VRChatPlus_Account").SetActive(false);
            GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_VRChat+/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Page_MM_Backgrounds").SetActive(true);
            GameObject.Find("Canvas_MainMenu(Clone)/Container/MMParent/HeaderOffset/Menu_VRChat+/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Page_MM_UIColorPalettes").SetActive(true);
        }
        void OnEnable()
        {
            DeepConsole.Log("Startup", "Yooo, i'm FakeVRCPlus.");
            Init().Start();
        }
        private IEnumerator Init()
        {
            while (GameObject.Find("Canvas_MainMenu(Clone)") == null)
            {
                yield return null;
            }
            GameObject.Find("Canvas_MainMenu(Clone)/Container/PageButtons/HorizontalLayoutGroup/Page_VRCPlusHighlight").GetComponent<Button1PublicObUnique>().onClick.AddListener((UnityEngine.Events.UnityAction)VRCPBEventButton);
            GameObject.Find("Canvas_MainMenu(Clone)/Container/PageButtons/HorizontalLayoutGroup/Page_VRCPlusHighlight").GetComponent<Button1PublicObUnique>().m_OnClick.AddListener((UnityEngine.Events.UnityAction)VRCPBEventButton);
        }
    }
}