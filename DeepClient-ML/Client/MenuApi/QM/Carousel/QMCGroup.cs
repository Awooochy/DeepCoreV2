namespace AstroClient.MenuApi.QM.Carousel
{
    using System;
    using AstroClient.ClientUI.QuickMenuGUI;
    using Controls;
    using Extras;
    using Items;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using xAstroBoy;
    using Object = UnityEngine.Object;

    public class QMCGroup : Root
    {
        public RectMask2D parentMenuMask { get; internal set; }
        public Transform MenuContents { get; internal set; }

        private readonly VerticalLayoutGroup Layout;

        public QMCGroup(Transform parent, string text, TextAnchor ButtonAlignment = TextAnchor.UpperLeft)
        {
            if (!APIBase.IsReady())
                throw new NullReferenceException("Object Search has FAILED!");

            (transform = (gameObject = Object.Instantiate(ApiPaths.QMCarouselPageTemplate, parent)).transform).name = $"QMCGroup_{text}";

            GameObject bg = ApiPaths.QMCarouselPageTemplate.transform.Find("QM_Settings_Panel/VerticalLayoutGroup/Background_Info").gameObject;
            Transform label = transform.Find("QM_Foldout/Label");
            label.GetComponent<TextMeshProUGUI>().text = text;
            Transform settingsPanel = transform.Find("QM_Settings_Panel/VerticalLayoutGroup");
            settingsPanel.DestroyChildren();
            GameObject newBG = Object.Instantiate(bg, settingsPanel);
            newBG.name = "Background_Info";

            (Layout = gameObject.GetComponent<VerticalLayoutGroup>()).childAlignment = ButtonAlignment;

            parentMenuMask = parent.parent.GetComponent<RectMask2D>();
        }
        public QMCGroup AddSpacer()
        {
            Transform s;
            (s = Object.Instantiate(ApiPaths.QMCarouselPageTemplate.transform.FindObject("Spacer"), this.GetTransform())).SetSiblingIndex(0);
            s.name = "Spacer";
            return this;
        }
        public void ChangeChildAlignment(TextAnchor ButtonAlignment = TextAnchor.UpperLeft) => Layout.childAlignment = ButtonAlignment;

        public QMCGroup(ClientPage page, string text, TextAnchor ButtonAlignment = TextAnchor.UpperLeft) : this(page.MenuContents, text, ButtonAlignment) { }

        public QMCToggle AddQMCToggle(string text, System.Action<bool> stateChange, string tooltip = "", bool defaultState = false, bool separator = false)
        {
            return new QMCToggle(this, text, stateChange, tooltip, defaultState, separator);
        }
        public QMCSlider AddQMCSlider(string text, string tooltip, Action<float, QMCSlider> listener, float defaultValue = 0f, float minValue = 0f, float maxValue = 100f, bool isDecimal = false, string ending = "%", bool separator = false)
        {
            return new QMCSlider(this, text, tooltip, listener, defaultValue, minValue, maxValue, isDecimal, ending, separator);
        }
        public QMCSelector AddQMCSelector(string text, string containerTooltip)
        {
            return new QMCSelector(this, text, containerTooltip);
        }
        public QMCFuncButton AddQMCFuncButton(string text, string tooltip, Action listener, bool rightContainer = false, bool separator = false, Sprite sprite = null)
        {
            return new QMCFuncButton(this, text, tooltip, listener, rightContainer, separator, sprite);
        }
        public QMCFuncToggle AddQMCFuncToggle(string text, System.Action<bool> listener, bool defaultState = false, bool rightContainer = false, bool separator = false, Sprite onSprite = null, Sprite offSprite = null)
        {
            return new QMCFuncToggle(this, text, listener, "", rightContainer, defaultState, separator, onSprite, offSprite);
        }
        public QMCTitle AddQMCTitle(string text, bool separator = false)
        {
            return new QMCTitle(this, text, separator);
        }

        internal void AddDuoButtons(string text1, string tp1, Action value1, string text2, string tp2, Action value2)
        {
            new QMCFuncButton(this, text1, tp1, value1, false, false).AddButton(text2, tp2, value2);
        }

        internal void AddGrpOfButtons(string text1, string tp1, Action value1, string text2, string tp2, Action value2, string text3, string tp3, Action value3)
        {
            new QMCFuncButton(this, text1, tp1, value1).AddButton(text2, tp2, value2).AddButton(text3, tp3, value3);
        }
    }

}

