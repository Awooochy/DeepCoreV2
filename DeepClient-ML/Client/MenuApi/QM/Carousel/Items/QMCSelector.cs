namespace AstroClient.MenuApi.QM.Carousel.Items
{
    using System;
    using System.Collections.Generic;
    using AstroClient.ClientUI.QuickMenuGUI;
    using Buttons.Groups;
    using Controls;
    using TMPro;
    using Tools;
    using UnityEngine;
    using UnityEngine.UI;
    using VRC.Localization;
    using VRC.UI.Elements.Controls;
    using Object = UnityEngine.Object;

    public class QMCSelector : QMCControl
    {
        public ToolTip ContainerTooltip { get; set; }
        public ToolTip SelectionBoxTextTooltip { get; set; }
        public TextMeshProUGUI TMProSelectionBoxText { get; set; }

        private List<Setting> settings = new List<Setting>();
        private int currentIndex = 0;
        public QMCSelector(Transform parent, string text, string containerTooltip, bool separator = false)
        {
            if (!APIBase.IsReady())
                throw new NullReferenceException("Object Search has FAILED!");

            (transform = (gameObject = Object.Instantiate(ApiPaths.QMCarouselSelectorTemplate, parent)).transform).name = text;

            Text = (TMProCompnt = transform.Find("LeftItemContainer/Title").GetComponent<TextMeshProUGUI>()).text = text;
            TMProCompnt.richText = true;

            if (separator != false)
                AddSeparator(parent);

            (ContainerTooltip = transform.Find("LeftItemContainer").GetComponent<ToolTip>())._localizableString =
                VRChatMenuExtensions.Localize(containerTooltip);

            Button buttonLeft = transform.Find("RightItemContainer/ButtonLeft").transform.GetComponent<Button>();
            buttonLeft.onClick.AddListener(new Action(() => ScrollLeft()));
            Button buttonRight = transform.Find("RightItemContainer/ButtonRight").transform.GetComponent<Button>();
            buttonRight.onClick.AddListener(new Action(() => ScrollRight()));

        }

        public QMCSelector AddSetting(string name, string tooltip, Action listener, bool invokeOnInit = true)
        {
            settings.Add(new Setting { Name = name, Tooltip = tooltip, Listener = listener });
            if (settings.Count == 1)
            {
                UpdateDisplayedSetting(0);
                TryInvoke(0, invokeOnInit);
            }
            return this;
        }
        public QMCSelector RemoveSetting(string name, bool invokeOnRemove = false)
        {
            int i = settings.FindIndex(s => s.Name == name);

            if (i == -1)
                throw new NullReferenceException($"Setting \"{name}\" Not Found Or Does Not Exist.");
            TryInvoke(currentIndex, invokeOnRemove);
            settings.RemoveAt(i);

            if (settings.Count > 0)
            {
                if (currentIndex >= i)
                {
                    currentIndex = Mathf.Clamp(currentIndex, 0, settings.Count - 1);
                }
                UpdateDisplayedSetting(currentIndex);
            }
            else
            {
                TMProSelectionBoxText.text = "N/A";
                SelectionBoxTextTooltip._localizableString = LocalizableStringExtensions.Localize("N/A");
            }

            return this;
        }
        public QMCSelector ClearSettings()
        {
            settings.Clear();
            TMProSelectionBoxText.text = "N/A";
            SelectionBoxTextTooltip._localizableString = LocalizableStringExtensions.Localize("N/A");
            return this;
        }
        public bool IsEmpty() => settings.Count == 0;
        private void ScrollLeft()
        {
            if (settings.Count == 0) return;
            currentIndex = (currentIndex - 1 + settings.Count) % settings.Count;
            UpdateDisplayedSetting(currentIndex);
            TryInvoke(currentIndex, true);
        }

        private void ScrollRight()
        {
            if (settings.Count == 0) return;
            currentIndex = (currentIndex + 1) % settings.Count;
            UpdateDisplayedSetting(currentIndex);
            TryInvoke(currentIndex, true);
        }

        private void UpdateDisplayedSetting(int index)
        {
            var setting = settings[index];
            (TMProSelectionBoxText = this.transform.Find("RightItemContainer/OptionSelectionBox/Text_MM_H3").GetComponent<TextMeshProUGUI>()).text = setting.Name;
            (SelectionBoxTextTooltip = this.transform.Find("RightItemContainer/OptionSelectionBox").GetComponent<ToolTip>())._localizableString = LocalizableStringExtensions.Localize(setting.Tooltip);
        }
        private void TryInvoke(int index, bool i)
        {
            if (i == true)
            {
                var setting = settings[index];
                setting.Listener.Invoke();
            }
        }


        private class Setting
        {
            public string Name { get; set; }
            public string Tooltip { get; set; }
            public Action Listener { get; set; }
        }
        public QMCSelector(QMCGroup group, string text, string containerTooltip, bool separator = false)
            : this(group.GetTransform().Find("QM_Settings_Panel/VerticalLayoutGroup").transform, text, containerTooltip, separator) { }

        public QMCSelector(CollapsibleButtonGroup buttonGroup, string text, string containerTooltip, bool separator = false)
            : this(buttonGroup.QMCParent, text, containerTooltip, separator) { }
    }

}

