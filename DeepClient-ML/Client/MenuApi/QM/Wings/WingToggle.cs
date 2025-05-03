namespace AstroClient.MenuApi.QM.Wings
{
    using System;
    using AstroClient.ClientUI.QuickMenuGUI;
    using AstroClient.Tools.Extensions;
    using TMPro;
    using Tools;
    using UnhollowerRuntimeLib;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.UI;
    using VRC.UI.Core.Styles;
    using VRC.UI.Elements.Controls;
    using xAstroBoy;
    using xAstroBoy.Utility;
    using Object = UnityEngine.Object;

    internal class WingToggle
    {
        internal GameObject IconObj { get; private set; }
        internal Image Icon { get; private set; }
        internal string Color { get; private set; }

        private GameObject button;
        private TextMeshProUGUI ButtonText;
        private string BtnText;
        private bool State;
        private Action<bool> Action;
        private string ToolTipText;
        private ToolTip _buttonToolTip;

        internal ToolTip ButtonToolTip
        {
            get
            {
                if (button == null)
                {
                    Log.Debug("WingToggle: Button is null in ButtonToolTip getter.");
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

        internal WingToggle(WingMenu parent, string btnText, string tooltip = null, Action<bool> action = null, Color? textColor = null, bool defaultState = false)
        {
            if (parent == null)
            {
                Log.Debug("WingToggle constructor: parent is null.");
                return;
            }

            string colorHtml = $"#{ColorUtility.ToHtmlStringRGB(textColor.GetValueOrDefault(UnityEngine.Color.white))}";
            InitButton(parent.VerticalLayoutGroup?.gameObject, btnText, tooltip, action, colorHtml, defaultState);
        }

        private void InitButton(GameObject parent, string btnText, string tooltip, Action<bool> action, string textColorHtml, bool defaultState)
        {
            if (parent == null)
            {
                Log.Debug("WingToggle.InitButton: parent GameObject is null.");
                return;
            }

            var template = ApiPaths.WingButtonTemplate_Right;
            if (template == null)
            {
                Log.Debug("WingToggle.InitButton: WingButtonTemplate_Right is null.");
                return;
            }

            button = Object.Instantiate(template, parent.transform);
            if (button == null)
            {
                Log.Debug("WingToggle.InitButton: Instantiation failed, button is null.");
                return;
            }

            MenuTools.EnableUIComponents(button);
            button.name = $"{BuildInfo.Name}_WingToggle_{btnText}";
            button.SetActive(true);

            Color = textColorHtml;
            BtnText = btnText;
            State = defaultState;
            Action = action;
            ToolTipText = tooltip;

            ButtonText = button.GetComponentInChildren<TextMeshProUGUI>();
            if (ButtonText == null)
            {
                Log.Debug("WingToggle.InitButton: TextMeshProUGUI component not found.");
                return;
            }
            SetupTextAutoSizing();

            var iconObjTransform = button.FindUIObject("Icon");
            if (iconObjTransform == null)
            {
                Log.Debug("WingToggle.InitButton: Icon child not found.");
            }
            else
            {
                IconObj = iconObjTransform.gameObject;
                Icon = IconObj.GetComponent<Image>();
                if (Icon == null)
                    Log.Debug("WingToggle.InitButton: Image component on IconObj not found.");

                var styleElement = IconObj.GetComponent<StyleElement>();
                if (styleElement != null)
                    Object.DestroyImmediate(styleElement);

                if (Icon != null)
                    Icon.color = UnityEngine.Color.white;
            }

            var arrow = button.FindUIObject("Icon_Arrow");
            if (arrow != null)
                Object.DestroyImmediate(arrow.gameObject);

            SetToolTip(tooltip);
            LoadSprite(null, false);
            ApplyText();
            SetupToggleAction();

            FixButton();
            UpdateGUI();
        }

        internal void LoadSprite(Sprite icon, bool refreshGui = true)
        {
            if (IconObj == null)
            {
                Log.Debug("WingToggle.LoadSprite: IconObj is null.");
            }
            else
            {
                IconObj.SetActive(icon != null);
            }

            if (icon != null)
            {
                if (Icon == null)
                    Log.Debug("WingToggle.LoadSprite: Icon component is null, cannot set sprite.");
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
                Log.Debug("WingToggle.UpdateGUI: button is null.");
                return;
            }

            var parentRect = button?.GetComponent<RectTransform>();
            if (parentRect == null)
            {
                Log.Debug("WingToggle.UpdateGUI: parent RectTransform is null.");
                return;
            }

            LayoutRebuilder.ForceRebuildLayoutImmediate(parentRect);
        }

        private void SetupTextAutoSizing()
        {
            if (ButtonText == null)
            {
                Log.Debug("WingToggle.SetupTextAutoSizing: ButtonText is null.");
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
                Log.Debug("WingToggle.SetupTextAutoSizing: RectTransform is null.");
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
                Log.Debug("WingToggle.UpdateTextLayout: ButtonText is null.");
                return;
            }

            var rt = ButtonText.rectTransform;
            float leftPadding = 0f;

            if (IconObj != null && IconObj.activeSelf)
            {
                var iconRt = IconObj.GetComponent<RectTransform>();
                if (iconRt != null)
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

        internal void SetToolTip(string text)
        {
            if (button == null)
            {
                Log.Debug("WingToggle.SetToolTip: button is null.");
                return;
            }
            ToolTipText = text;
            var tt = ButtonToolTip;
            if (tt == null)
            {
                Log.Debug("WingToggle.SetToolTip: ButtonToolTip is null.");
                return;
            }
            tt._localizableString = ApiToolsExts.Localize(text);
        }

        private void ApplyText()
        {
            if (ButtonText == null)
            {
                Log.Debug("WingToggle.ApplyText: ButtonText is null.");
                return;
            }

            string onOff = State ? " <color=green>ON</color>" : " <color=red>OFF</color>";
            ButtonText.text = $"<color={Color}>{BtnText}</color>{onOff}";
        }

        private void SetupToggleAction()
        {
            if (button == null)
            {
                Log.Debug("WingToggle.SetupToggleAction: button is null.");
                return;
            }

            var btnComp = button.GetComponent<Button>();
            if (btnComp == null)
            {
                Log.Debug("WingToggle.SetupToggleAction: Button component not found.");
                return;
            }

            btnComp.onClick = new Button.ButtonClickedEvent();
            btnComp.onClick.AddListener(DelegateSupport.ConvertDelegate<UnityAction>(() =>
            {
                State = !State;
                ApplyText();
                try
                {
                    Action?.SafetyRaise(State);
                }
                catch
                {
                    Log.Debug("WingToggle toggle action threw an exception.");
                }
            }));
        }

        internal void FixButton()
        {
            if (button == null)
            {
                Log.Debug("WingToggle.FixButton: button is null.");
                return;
            }

            var rect = button.GetComponent<RectTransform>();
            if (rect == null)
            {
                Log.Debug("WingToggle.FixButton: RectTransform not found.");
                return;
            }

            var layout = button.GetOrAddComponent<LayoutElement>();
            if (layout == null)
            {
                Log.Debug("WingToggle.FixButton: LayoutElement could not be added.");
                return;
            }

            layout.preferredWidth = 356f;
            layout.preferredHeight = 112f;
            layout.minWidth = 356f;
            layout.minHeight = 112f;
            layout.flexibleWidth = 0;
            layout.flexibleHeight = 0;
        }

        internal void SetToggleState(bool state, bool shouldInvoke = false)
        {
            State = state;
            ApplyText();
            if (shouldInvoke)
            {
                try { Action?.SafetyRaise(State); }
                catch { Log.Debug("WingToggle.SetToggleState: SafetyRaise threw an exception."); }
            }
        }

        internal void SetBackgroundColor(Color backgroundColor)
        {
            var img = button?.GetComponentInChildren<Image>();
            if (img == null)
            {
                Log.Debug("WingToggle.SetBackgroundColor: Image not found.");
                return;
            }
            img.color = backgroundColor;
        }

        internal void SetTextColor(Color textColor)
        {
            Color = $"#{ColorUtility.ToHtmlStringRGB(textColor)}";
            ApplyText();
        }

        internal void SetTextColorHtml(string htmlColor)
        {
            Color = htmlColor;
            ApplyText();
        }

        internal void DestroyMe()
        {
            if (button == null)
            {
                Log.Debug("WingToggle.DestroyMe: button is null.");
                return;
            }
            Object.Destroy(button);
        }

        internal void SetActive(bool isActive)
        {
            if (button == null)
            {
                Log.Debug("WingToggle.SetActive: button is null.");
                return;
            }
            button.SetActive(isActive);
        }

        internal void SetInteractable(bool interactable)
        {
            var btnComp = button?.GetComponent<Button>();
            if (btnComp == null)
            {
                Log.Debug("WingToggle.SetInteractable: Button component not found.");
                return;
            }
            btnComp.interactable = interactable;
        }
    }
}
