namespace AstroClient.MenuApi.QM.Wings
{
    #region

    using System;
    using ClientUI.QuickMenuGUI;
    using Extras;
    using TMPro;
    using Tools;
    using UnhollowerRuntimeLib;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using VRC.UI.Core.Styles;
    using VRC.UI.Elements;
    using VRC.UI.Elements.Controls;
    using xAstroBoy;
    using xAstroBoy.Utility;
    using Color = UnityEngine.Color;
    using Image = UnityEngine.UI.Image;
    using Object = UnityEngine.Object;

    #endregion

    internal class WingMenu
    {
        private readonly bool isLeftWing;
        private readonly bool isSubPage;
        private readonly WingMenu parent;
        private ToolTip _buttonToolTip;
        private bool isOpened;

        public WingMenu(string name, string btnToolTip = "", Sprite icon = null, bool left = true)
            : this(null, name, btnToolTip, icon, left, false)
        {
        }

        public WingMenu(WingMenu menu, string name, string btnToolTip = "", Sprite icon = null)
            : this(menu, name, btnToolTip, icon, menu?.isLeftWing ?? true, true)
        {
        }

        private WingMenu(WingMenu menu, string name, string btnToolTip, Sprite icon, bool left, bool sub)
        {
            parent = menu;
            isLeftWing = left;
            isSubPage = sub;
            MenuName = name;
            Initialize(btnToolTip, icon);
        }

        internal GameObject Arrow_Icon { get; private set; }
        internal GameObject IconObj { get; private set; }
        internal Image Icon { get; private set; }
        internal GameObject button { get; private set; }
        internal Button BackButton { get; private set; }
        internal Button OpenButton { get; private set; }
        internal TextMeshProUGUIEx ButtonText { get; private set; }
        internal TextMeshProUGUIEx ButtonText_Title { get; private set; }
        internal UIPage CurrentPage { get; private set; }
        internal string ToolTipText { get; private set; }
        internal MenuStateController CurrentController { get; private set; }
        internal GameObject VerticalLayoutGroup { get; private set; }

        internal string MenuName { get; }

        internal ToolTip ButtonToolTip
        {
            get
            {
                if (button == null)
                {
                    Log.Debug("WingMenu.ButtonToolTip: button is null.");
                    return null;
                }

                _buttonToolTip =
                    button.GetComponent<ToolTip>() ??
                    button.GetComponentInChildren<ToolTip>(true) ??
                    button.GetComponentInParent<ToolTip>();
                return _buttonToolTip;
            }
        }

        internal Action OnWingOpen { get; set; }
        internal Action OnWingClose { get; set; }

        private void Initialize(string tooltip, Sprite icon)
        {
            var templateBtn = isLeftWing ? ApiPaths.WingButtonTemplate_Left : ApiPaths.WingButtonTemplate_Right;
            var controller = isLeftWing ? ApiPaths.Wing_Left_MenuController : ApiPaths.Wing_Right_MenuController;
            var pageComp = isLeftWing ? ApiPaths.UIPage_WingLeft_Template : ApiPaths.UIPage_WingRight_Template;
            GameObject layoutRoot = null;

            if (isSubPage)
            {
                layoutRoot = parent?.VerticalLayoutGroup;
                if (layoutRoot == null)
                {
                    Log.Debug("WingMenu.Initialize: subPage but parent VerticalLayoutGroup is null.");
                }
            }
            else
            {
                layoutRoot = isLeftWing ? ApiPaths.WingParentL : ApiPaths.WingParentR;
            }

            if (templateBtn == null || controller == null || pageComp == null || layoutRoot == null)
            {
                Log.Debug("WingMenu.Initialize: one or more API paths/controllers/pages/layouts are null.");
                return;
            }

            CreateButton(templateBtn, controller, pageComp, layoutRoot, tooltip, icon);
        }

        private void CreateButton(GameObject btnTemplate, MenuStateController controller, UIPage pageComp,
            GameObject layoutRoot, string tooltip, Sprite icon)
        {
            button = Object.Instantiate(btnTemplate, layoutRoot.transform);
            if (button == null)
            {
                Log.Debug("WingMenu.CreateButton: instantiation failed.");
                return;
            }

            button.name = BuildInfo.Name + (isSubPage ? "WingSubPage" : "WingPage");

            ButtonText = button?.NewText("Text_QM_H3");
            if (ButtonText == null)
            {
                Log.Debug("WingMenu.CreateButton: ButtonText not created.");
                return;
            }

            ButtonText.text = MenuName;
            ButtonText.richText = true;

            SetToolTip(tooltip);

            CurrentController = controller;
            CurrentPage = pageComp?.GeneratePage(controller, MenuName);
            if (CurrentPage == null)
            {
                Log.Debug("WingMenu.CreateButton: CurrentPage generation failed.");
                return;
            }

            ButtonText_Title = CurrentPage.gameObject?.NewText("Text_QM_H2 (1)");
            if (ButtonText_Title == null)
            {
                ButtonText_Title = CurrentPage.gameObject?.NewText("Text_Title");
            }

            if (ButtonText_Title == null)
            {
                Log.Debug("WingMenu.CreateButton: Title text not created.");
            }
            else
            {
                ButtonText_Title.text = MenuName;
            }

            SetupTextAutoSizing();
            SetupLayout(CurrentPage);

            VerticalLayoutGroup = CurrentPage.gameObject?.FindUIObject("VerticalLayoutGroup");
            if (VerticalLayoutGroup != null)
            {
                QMUtils.DestroyChildren(VerticalLayoutGroup);
            }

            OpenButton = button.GetComponent<Button>();
            BackButton = FindBackButton(CurrentPage);
            if (OpenButton == null || BackButton == null)
            {
                Log.Debug("WingMenu.CreateButton: Open or Back button missing.");
            }

            OpenButton.onClick = new Button.ButtonClickedEvent();
            OpenButton.onClick.AddListener(DelegateSupport.ConvertDelegate<UnityAction>(() =>
            {
                ShowPage();
                isOpened = true;
                OnWingOpen?.SafetyRaise();
            }));

            BackButton.onClick = new Button.ButtonClickedEvent();
            BackButton.onClick.AddListener(DelegateSupport.ConvertDelegate<UnityAction>(() =>
            {
                ReturnToParent();
                isOpened = false;
                OnWingClose?.SafetyRaise();
            }));

            var iconTf = button.FindUIObject("Icon");
            IconObj = iconTf?.gameObject;
            Icon = IconObj?.GetComponent<Image>();
            if (IconObj == null || Icon == null)
            {
                Log.Debug("WingMenu.CreateButton: Icon object or component missing.");
            }
            else
            {
                var styleEl = IconObj.GetComponent<StyleElement>();
                if (styleEl != null)
                {
                    Object.DestroyImmediate(styleEl);
                }

                Icon.color = Color.white;
            }

            LoadSprite(icon, false);
            Arrow_Icon = button.FindUIObject("Icon_Arrow");
            CurrentPage.gameObject?.ToggleScrollRectOnExistingMenu(true);
            CurrentPage.gameObject?.SetActive(false);

            FixButton();
            UpdateGUI();
            button.SetActive(true);
        }

        internal void LoadSprite(Sprite icon, bool refreshGui = true)
        {
            if (IconObj == null)
            {
                Log.Debug("WingMenu.LoadSprite: IconObj is null.");
            }
            else
            {
                IconObj.SetActive(icon != null);
            }

            if (icon != null)
            {
                if (Icon == null)
                {
                    Log.Debug("WingMenu.LoadSprite: Icon component is null.");
                }
                else
                {
                    Icon.sprite = icon;
                }
            }

            UpdateTextLayout();
            if (refreshGui)
            {
                UpdateGUI();
            }
        }

        private void UpdateGUI()
        {
            if (button == null)
            {
                Log.Debug("WingMenu.UpdateGUI: button is null.");
                return;
            }

            var parentRect = button?.GetComponent<RectTransform>();
            if (parentRect == null)
            {
                Log.Debug("WingMenu.UpdateGUI: parent RectTransform is null.");
                return;
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(parentRect);
        }

        private void SetupTextAutoSizing()
        {
            if (ButtonText == null)
            {
                Log.Debug("WingMenu.SetupTextAutoSizing: ButtonText is null.");
                return;
            }

            ButtonText.richText = true;
            ButtonText.enableWordWrapping = true;
            ButtonText.enableAutoSizing = true;
            ButtonText.fontSizeMin = 15;
            ButtonText.fontSizeMax = 35;
            ButtonText.overflowMode = TextOverflowModes.Ellipsis;
            var rt = ButtonText.rectTransform;
            if (rt == null)
            {
                Log.Debug("WingMenu.SetupTextAutoSizing: RectTransform is null.");
                return;
            }

            rt.anchorMin = Vector2.zero;
            rt.anchorMax = Vector2.one;
            rt.pivot = new Vector2(0.5f, 0.5f);
        }

        private void UpdateTextLayout()
        {
            if (ButtonText == null)
            {
                Log.Debug("WingMenu.UpdateTextLayout: ButtonText is null.");
                return;
            }

            var rt = ButtonText.rectTransform;
            if (rt == null)
            {
                Log.Debug("WingMenu.UpdateTextLayout: RectTransform is null.");
                return;
            }

            var leftPadding = 0f;
            if (IconObj != null && IconObj.activeSelf)
            {
                var iconRt = IconObj.GetComponent<RectTransform>();
                if (iconRt == null)
                {
                    Log.Debug("WingMenu.UpdateTextLayout: IconObj RectTransform is null.");
                    return;
                }

                leftPadding = iconRt.sizeDelta.x + 8f;
                ButtonText.alignment = TextAlignmentOptions.Left;
            }
            else
            {
                ButtonText.alignment = TextAlignmentOptions.Center;
            }

            rt.offsetMin = new Vector2(leftPadding, 0f);
            rt.offsetMax = new Vector2(0f, 0f);
        }

        internal void FixButton()
        {
            var rect = button?.GetComponent<RectTransform>();
            if (rect == null)
            {
                Log.Debug("WingMenu.FixButton: RectTransform is null.");
                return;
            }

            var layout = button.GetOrAddComponent<LayoutElement>();
            if (layout == null)
            {
                Log.Debug("WingMenu.FixButton: LayoutElement could not be added.");
                return;
            }

            layout.preferredWidth = 356f;
            layout.preferredHeight = 112f;
            layout.minWidth = 356f;
            layout.minHeight = 112f;
            layout.flexibleWidth = 0;
            layout.flexibleHeight = 0;
        }

        private void ShowPage()
        {
            if (isLeftWing)
            {
                ApiPaths.Wing_Left_MenuController?.ShowTabContent(MenuName);
            }
            else
            {
                ApiPaths.Wing_Right_MenuController?.ShowTabContent(MenuName);
            }

            CurrentPage?.gameObject.SetActive(true);
        }

        private void ReturnToParent()
        {
            if (parent == null)
            {
                if (isLeftWing)
                {
                    ApiPaths.Wing_Left_MenuController?.ShowTabContent("Root");
                }
                else
                {
                    ApiPaths.Wing_Right_MenuController?.ShowTabContent("Root");
                }
            }
            else
            {
                parent.ShowPage();
            }

            CurrentPage?.gameObject.SetActive(false);
        }

        private static void SetupLayout(UIPage page)
        {
            var vl = page?.GetComponentInChildren<VerticalLayoutGroup>();
            if (vl == null)
            {
                Log.Debug("WingMenu.SetupLayout: VerticalLayoutGroup not found.");
                return;
            }

            vl.spacing = 12;
            vl.m_Spacing = 12;
            vl.childControlHeight = true;
            vl.childControlWidth = false;
            vl.childScaleHeight = false;
            vl.childScaleWidth = false;
        }

        private static Button FindBackButton(UIPage page)
        {
            if (page == null)
            {
                Log.Debug("WingMenu.FindBackButton: page is null.");
                return null;
            }

            var obj = page.transform.FindObject("WngHeader_H1/LeftItemContainer/Button_Back");
            if (obj == null)
            {
                obj = page.gameObject.FindUIObject("Button_Back")?.transform;
            }

            if (obj == null)
            {
                Log.Debug("WingMenu.FindBackButton: back button not found.");
                return null;
            }

            return obj.GetComponent<Button>();
        }

        internal void SetToolTip(string text)
        {
            ToolTipText = text;
            var tt = ButtonToolTip;
            if (tt == null)
            {
                Log.Debug("WingMenu.SetToolTip: ToolTip is null.");
                return;
            }

            tt._localizableString = text.Localize();
        }

        internal void SetActive(bool isActive, bool openMenu = false)
        {
            if (button == null)
            {
                Log.Debug("WingMenu.SetActive: button is null.");
                return;
            }

            if (!isActive && isOpened)
            {
                Close();
            }

            button.SetActive(isActive);
            if (isActive && openMenu)
            {
                Open();
            }
        }

        internal void Open()
        {
            if (OpenButton == null)
            {
                Log.Debug("WingMenu.Open: OpenButton is null.");
                return;
            }

            if (OpenButton.onClick == null)
            {
                Log.Debug("WingMenu.Open: OpenButton onClick is null.");
                return;
            }

            OpenButton.onClick.Invoke();
        }

        internal void Close()
        {
            if (BackButton == null)
            {
                Log.Debug("WingMenu.Close: BackButton is null.");
                return;
            }

            if (BackButton.onClick == null)
            {
                Log.Debug("WingMenu.Open: OpenButton onClick is null.");
                return;
            }

            BackButton.onClick.Invoke();
        }

        internal void SetInteractable(bool interactable)
        {
            if (OpenButton == null)
            {
                Log.Debug("WingMenu.SetInteractable: OpenButton is null.");
                return;
            }

            OpenButton.interactable = interactable;
        }

        internal void Destroy()
        {
            if (button != null)
            {
                Object.Destroy(button);
            }

            if (isLeftWing)
            {
                ApiPaths.Wing_Left_MenuController?.RemovePage(CurrentPage);
            }
            else
            {
                ApiPaths.Wing_Right_MenuController?.RemovePage(CurrentPage);
            }
        }

        internal void SetTextColor(Color color)
        {
            if (ButtonText == null)
            {
                Log.Debug("WingMenu.SetTextColor: ButtonText is null.");
                return;
            }

            var html = $"#{ColorUtility.ToHtmlStringRGB(color)}";
            ButtonText.text = $"<color={html}>{ButtonText.text}</color>";
        }

        internal void SetButtonText(string text)
        {
            if (ButtonText == null)
            {
                Log.Debug("WingMenu.SetButtonText: ButtonText is null.");
                return;
            }

            ButtonText.text = text;
        }

        internal WingMenu AddPage(string name, string btnToolTip, Sprite icon = null)
        {
            return new WingMenu(this, name, btnToolTip, icon);
        }

        internal WingButton AddButton(string btnText, string btnToolTip = "", Action btnAction = null,
            Color? textColor = null)
        {
            return new WingButton(this, btnText, btnAction, btnToolTip, textColor);
        }

        internal WingToggle AddToggle(string btnText, string btnToolTip = "", Action<bool> action = null,
            Color? textColor = null)
        {
            return new WingToggle(this, btnText, btnToolTip, action, textColor);
        }
    }
}