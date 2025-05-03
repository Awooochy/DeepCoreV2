namespace AstroClient.MenuApi.QM
{
    using System;
    using System.Linq;
    using AstroClient.ClientUI.QuickMenuGUI;
    using Controls;
    using Extras;
    using TMPro;
    using Tools;
    using UnityEngine;
    using UnityEngine.UI;
    using VRC.Localization;
    using VRC.UI.Core.Styles;
    using VRC.UI.Elements;
    using VRC.UI.Elements.Controls;
    using VRC.UI.Elements.Menus;
    using xAstroBoy;
    using xAstroBoy.Utility;
    using Object = UnityEngine.Object;

    public class VRCPage : ClientPage
    {
        public bool IsRoot { get; set; } // This should be fine to edit
        public static VRCPage lastOpenedPage { get; private set; }

        public Action BackButtonPress;
        public TextMeshProUGUI pageTitleText;
        public RectMask2D menuMask;

        private GameObject extButtonGameObject;

        public VRCPage(string pageTitle, bool root = false, bool backButton = true, bool expandButton = false, Action expandButtonAction = null, string expandButtonTooltip = "", Sprite expandButtonSprite = null, bool preserveColor = false)
        {
            if (!APIBase.IsReady()) throw new Exception();
            if (ApiPaths.Menu_Camera == null)
            {
                //Logs.Error("Fatal Error: ButtonAPI.menuPageBase Is Null!");
                return;
            }

            var region = 0;
            MenuName = $"{BuildInfo.Name}_Menu_{pageTitle}_{Guid.NewGuid()}";
            IsRoot = root;

            try
            {
                var gameObject = Object.Instantiate(ApiPaths.Menu_Camera, ApiPaths.Menu_Camera.transform.parent);
                gameObject.name = MenuName;
                gameObject.transform.SetSiblingIndex(9); //Changed from 6 to be put 2 game objects after the camera menu (unnecessary but im doing it anyways)
                gameObject.gameObject.active = false;

                region++;
                //Object.DestroyImmediate(gameObject.GetOrAddComponent<VRC.UI.Elements.Menus.MainMenuContent>()); //changed to remove the Camera menu stuff instead of the launchpad menu stuff
                Object.DestroyImmediate(gameObject.GetOrAddComponent<CameraMenu>());
                (Page = gameObject.gameObject.AddComponent<UIPage>()).field_Public_String_0 = MenuName;
                region++;

                Page.field_Private_Boolean_1 = true;
                //Page.field_Protected_MenuStateController_0 = QMUtils.GetMenuStateControllerInstance;
                Page.field_Private_List_1_UIPage_0 = new Il2CppSystem.Collections.Generic.List<UIPage>();
                Page.field_Private_List_1_UIPage_0.Add(Page);

                region++;
                ApiPaths.QuickMenu_MenuController.field_Private_Dictionary_2_String_UIPage_0.Add(MenuName, Page);
                if (root)
                {
                    var list = ApiPaths.QuickMenu_MenuController.field_Public_ArrayOf_UIPage_0.ToList();
                    list.Add(Page);
                    ApiPaths.QuickMenu_MenuController.field_Public_ArrayOf_UIPage_0 = list.ToArray();
                }
                region++;

                (MenuContents = gameObject.transform.Find("Scrollrect/Viewport/VerticalLayoutGroup")).GetComponent<HorizontalOrVerticalLayoutGroup>().childControlHeight = true;
                QMUtils.DestroyChildren(MenuContents);

                region++;
                (pageTitleText = gameObject.FindObject("Header_Camera/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUI>()).fontSize = 54.7f;
                pageTitleText.text = pageTitle;
                pageTitleText.richText = true;
                region++;

                var backButtonGameObject = gameObject.transform.GetChild(0).Find("LeftItemContainer/Button_Back").gameObject;
                backButtonGameObject.SetActive(backButton);
                (backButtonGameObject.GetComponentInChildren<Button>().onClick = new Button.ButtonClickedEvent()).AddListener(new Action(() => {
                    if (IsRoot) ApiPaths.QuickMenu_MenuController.OpenPage("QuickMenuDashboard");
                    else Page.Method_Protected_Virtual_New_Void_0();
                    OnMenuClose.SafetyRaise();
                    BackButtonPress.SafetyRaise();
                }));

                region++;
                (extButtonGameObject = gameObject.transform.GetChild(0).Find("RightItemContainer/Button_QM_Expand").gameObject).SetActive(expandButton);
                extButtonGameObject.GetComponentInChildren<Button>().onClick = new Button.ButtonClickedEvent();
                if (expandButtonAction != null)
                    extButtonGameObject.GetComponentInChildren<Button>().onClick.AddListener(expandButtonAction);

                if (expandButtonSprite != null)
                {
                    extButtonGameObject.GetComponentInChildren<Image>().sprite = expandButtonSprite;
                    extButtonGameObject.GetComponentInChildren<Image>().overrideSprite = expandButtonSprite;
                    if (preserveColor)
                    {
                        extButtonGameObject.GetComponentInChildren<Image>().color = Color.white;
                        extButtonGameObject.GetComponentInChildren<StyleElement>(true).enabled = false;
                    }
                }
                region++;

                (menuMask = MenuContents.parent.gameObject.GetOrAddComponent<VRCRectMask2D>()).enabled = true;
                gameObject.transform.Find("Scrollrect").GetOrAddComponent<VRCScrollRect>().enabled = true;
                gameObject.transform.Find("Scrollrect").GetOrAddComponent<ScrollRect>().verticalScrollbar = gameObject.transform.Find("Scrollrect/Scrollbar").GetOrAddComponent<Scrollbar>();
                gameObject.transform.Find("Scrollrect").GetOrAddComponent<ScrollRect>().verticalScrollbarVisibility = ScrollRect.ScrollbarVisibility.AutoHide;
                QMUtils.DestroyChildren(gameObject, where => where.name != "Scrollrect" && where.name != "Header_Camera");
                region++;

                gameObject.transform.Find("Scrollrect/Viewport").GetComponent<VRCRectMask2D>().prop_Boolean_0 = true; // Fixes the items falling off of the QM
                gameObject.transform.Find("Scrollrect").GetComponent<VRC.UI.Elements.Controls.VRCScrollRect>().field_Public_Boolean_0 = true; // Fixes the items falling off of the QM

                region++;
                Page.GetComponent<Canvas>().enabled = true; // Fix for Late Menu Creation
                Page.GetComponent<CanvasGroup>().enabled = true; // Fix for Late Menu Creation
                Page.GetComponent<UIPage>().enabled = true; // Fix for Late Menu Creation
                Page.GetComponent<GraphicRaycaster>().enabled = true; // Fix for Late Menu Creation
            }
            catch (System.Exception ex)
            {
                throw new Exception("Exception Caught When Making Page At Region: " + region + "\n\n" + ex);
            }
        }

        public void AddExtButton(Action onClick, string tooltip, Sprite icon)
        {
            var obj = Object.Instantiate(extButtonGameObject, extButtonGameObject.transform.parent);
            obj.transform.SetSiblingIndex(0);
            obj.SetActive(true);
            obj.GetComponentInChildren<Button>().onClick = new Button.ButtonClickedEvent();
            obj.GetComponentInChildren<Button>().onClick.AddListener(onClick);
            obj.GetComponent<ToolTip>()._localizableString = VRChatMenuExtensions.Localize(tooltip);
            obj.GetComponentInChildren<Image>().sprite = icon;
            obj.GetComponentInChildren<Image>().overrideSprite = icon;
        }


        public void OpenMenu()
        {
            int temp = 0;
            try
            {
                temp++;
                Page.gameObject.active = true;
                temp++;
                ApiPaths.QuickMenu_MenuController.OpenPage(Page.field_Public_String_0);
                temp++;
                temp++;
                lastOpenedPage = this;
                OnMenuOpen.SafetyRaise();
                temp++;
            }
            catch (System.Exception ex)
            {
                throw new Exception("Exception Caught When Opening Page\n\n" + ex + "   " + temp);
            }
        }

        public void SetTitle(string text) => pageTitleText.text = text;

        public void CloseMenu()
        {
            Page.Method_Protected_Virtual_New_Void_0();
            OnMenuClose.SafetyRaise();
        }
    }
}

