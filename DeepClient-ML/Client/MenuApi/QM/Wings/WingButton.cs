namespace AstroClient.MenuApi.QM.Wings
{
    using System;
    using System.Linq;
    using AstroClient.xAstroBoy.Utility;
    using ClientUI.QuickMenuGUI;
    using TMPro;
    using Tools;
    using UnhollowerRuntimeLib;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using VRC.UI.Core.Styles;
    using VRC.UI.Elements.Controls;
    using xAstroBoy;
    using Object = UnityEngine.Object;

    internal class WingButton
    {
        internal GameObject IconObj { get; private set; }
        internal Image Icon { get; private set; }
        internal string Color { get; set; }
        internal string Text { get; set; }
        internal TextMeshProUGUI ButtonText { get; private set; }

        private GameObject button;
        private string btnType;
        private string ToolTipText;
        private ToolTip _buttonToolTip;

        internal ToolTip ButtonToolTip
        {
            get
            {
                if (button == null)
                {
                    Log.Debug("WingButton.ButtonToolTip: button is null.");
                    return null;
                }
                if (_buttonToolTip == null)
                    _buttonToolTip =
                        button.GetComponent<ToolTip>() ??
                        button.GetComponentInChildren<ToolTip>(true) ??
                        button.AddComponent<ToolTip>();
                return _buttonToolTip;
            }
        }

        public WingButton(WingMenu parent, string btnText, Action btnAction, string btnToolTip, Color? textColor = null)
        {
            if (parent == null)
            {
                Log.Debug("WingButton constructor: parent is null.");
                return;
            }
            string colorHtml = $"#{ColorUtility.ToHtmlStringRGB(textColor.GetValueOrDefault(UnityEngine.Color.white))}";
            InitButton(parent.VerticalLayoutGroup?.gameObject, btnText, btnAction, btnToolTip, colorHtml);
        }

        private void InitButton(GameObject parent, string btnText, Action btnAction, string btnToolTip, string textColorHtml)
        {
            if (parent == null)
            {
                Log.Debug("WingButton.InitButton: parent is null.");
                return;
            }
            var template = ApiPaths.WingButtonTemplate_Left;
            if (template == null)
            {
                Log.Debug("WingButton.InitButton: template is null.");
                return;
            }
            button = Object.Instantiate(template, parent.transform);
            if (button == null)
            {
                Log.Debug("WingButton.InitButton: instantiation failed.");
                return;
            }

            MenuTools.EnableUIComponents(button);
            btnType = "WingButton";
            button.name = $"{BuildInfo.Name}_{btnType}_{btnText}";
            button.SetActive(true);

            Color = textColorHtml;
            Text = btnText;

            ButtonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (ButtonText == null)
            {
                Log.Debug("WingButton.InitButton: TextMeshProUGUI not found.");
            }
            else
            {
                SetupTextAutoSizing();
            }

            var iconTransform = button.FindUIObject("Icon");
            if (iconTransform == null)
            {
                Log.Debug("WingButton.InitButton: Icon child not found.");
            }
            else
            {
                IconObj = iconTransform.gameObject;
                Icon = IconObj.GetComponent<Image>();
                if (Icon == null)
                    Log.Debug("WingButton.InitButton: Image on IconObj not found.");
                var style = IconObj.GetComponent<StyleElement>();
                if (style != null)
                    Object.DestroyImmediate(style);
                if (Icon != null)
                    Icon.color = UnityEngine.Color.white;
            }

            var arrow = button.FindUIObject("Icon_Arrow");
            if (arrow != null)
                Object.DestroyImmediate(arrow.gameObject);

            SetToolTip(btnToolTip);
            SetText(btnText);
            LoadSprite(null, false);
            SetAction(btnAction);
            FixButton();
            UpdateGUI();
        }

        internal void LoadSprite(Sprite icon, bool refreshGui = true)
        {
            if (IconObj == null)
            {
                Log.Debug("WingButton.LoadSprite: IconObj is null.");
            }
            else
            {
                IconObj.SetActive(icon != null);
            }
            if (icon != null)
            {
                if (Icon == null)
                    Log.Debug("WingButton.LoadSprite: Icon is null.");
                else
                    Icon.sprite = icon;
            }

            UpdateTextLayout();
            if (refreshGui)
                UpdateGUI();
        }

        private void UpdateGUI()
        {
            if (button == null)
            {
                Log.Debug("WingButton.UpdateGUI: button is null.");
                return;
            }

            var parentRect = button?.GetComponent<RectTransform>();
            if (parentRect == null)
            {
                Log.Debug("WingButton.UpdateGUI: parent RectTransform is null.");
                return;
            }
            LayoutRebuilder.ForceRebuildLayoutImmediate(parentRect);
        }

        private void SetupTextAutoSizing()
        {
            if (ButtonText == null)
            {
                Log.Debug("WingButton.SetupTextAutoSizing: ButtonText is null.");
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
                Log.Debug("WingButton.SetupTextAutoSizing: RectTransform is null.");
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
                Log.Debug("WingButton.UpdateTextLayout: ButtonText is null.");
                return;
            }
            var rt = ButtonText.rectTransform;
            if (rt == null)
            {
                Log.Debug("WingButton.UpdateTextLayout: RectTransform is null.");
                return;
            }
            float leftPadding = 0f;
            if (IconObj != null && IconObj.activeSelf)
            {
                var iconRt = IconObj.GetComponent<RectTransform>();
                if (iconRt == null)
                {
                    Log.Debug("WingButton.UpdateTextLayout: IconObj RectTransform is null.");
                }
                else
                {
                    leftPadding = iconRt.sizeDelta.x + 8f;
                    ButtonText.alignment = TextAlignmentOptions.Left;
                }
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
            if (button == null)
            {
                Log.Debug("WingButton.FixButton: button is null.");
                return;
            }
            var rect = button.GetComponent<RectTransform>();
            if (rect == null)
            {
                Log.Debug("WingButton.FixButton: RectTransform is null.");
                return;
            }
            var layout = button.GetOrAddComponent<LayoutElement>();
            if (layout == null)
            {
                Log.Debug("WingButton.FixButton: LayoutElement could not be added.");
                return;
            }
            layout.preferredWidth = 356f;
            layout.preferredHeight = 112f;
            layout.minWidth = 356f;
            layout.minHeight = 112f;
            layout.flexibleWidth = 0;
            layout.flexibleHeight = 0;
        }

        internal void SetToolTip(string text)
        {
            ToolTipText = text;
            var tt = ButtonToolTip;
            if (tt == null)
            {
                Log.Debug("WingButton.SetToolTip: ToolTip is null.");
                return;
            }
            tt._localizableString = text.Localize();
        }

        internal void SetText(string buttonText)
        {
            Text = buttonText;
            if (ButtonText == null)
            {
                Log.Debug("WingButton.SetText: ButtonText is null.");
                return;
            }
            ButtonText.text = $"<color={Color}>{Text}</color>";
        }

        internal void SetAction(Action buttonAction)
        {
            if (button == null)
            {
                Log.Debug("WingButton.SetAction: button is null.");
                return;
            }
            var btnComp = button.GetComponent<Button>();
            if (btnComp == null)
            {
                Log.Debug("WingButton.SetAction: Button component not found.");
                return;
            }
            btnComp.onClick = new Button.ButtonClickedEvent();
            if (buttonAction != null)
                btnComp.onClick.AddListener(DelegateSupport.ConvertDelegate<UnityAction>(buttonAction));
        }

        internal void SetActive(bool isActive)
        {
            if (button == null)
            {
                Log.Debug("WingButton.SetActive: button is null.");
                return;
            }
            button.SetActive(isActive);
        }

        internal void DestroyMe()
        {
            if (button == null)
            {
                Log.Debug("WingButton.DestroyMe: button is null.");
                return;
            }
            Object.Destroy(button);
        }

        internal void SetInteractable(bool isInteractable)
        {
            if (button == null)
            {
                Log.Debug("WingButton.SetInteractable: button is null.");
                return;
            }
            var btnComp = button.GetComponent<Button>();
            if (btnComp == null)
            {
                Log.Debug("WingButton.SetInteractable: Button component not found.");
                return;
            }
            btnComp.interactable = isInteractable;
        }

        internal void SetTextColor(Color buttonTextColor)
        {
            Color = $"#{ColorUtility.ToHtmlStringRGB(buttonTextColor)}";
            SetText(Text);
        }

        internal void SetTextColorHtml(string buttonTextColor)
        {
            Color = buttonTextColor;
            SetText(Text);
        }
    }
}
