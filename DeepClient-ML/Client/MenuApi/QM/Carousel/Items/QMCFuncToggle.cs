namespace AstroClient.MenuApi.QM.Carousel.Items
{
    using System;
    using AstroClient.ClientUI.QuickMenuGUI;
    using Buttons.Groups;
    using ClientResources.Loaders;
    using Controls;
    using Extras;
    using TMPro;
    using Tools;
    using UnityEngine;
    using UnityEngine.UI;
    using VRC.Localization;
    using VRC.UI.Elements.Controls;
    using Object = UnityEngine.Object;

    public class QMCFuncToggle : QMCControl
    {
        public System.Action<bool> Listener { get; set; }
        public bool isToggled { get; private set; }
        public Image ToggleSprite;
        public Transform ButtonParent { get; private set; }
        public static Transform leftPar { get; private set; }
        public static Transform rightPar { get; private set; }
        public Sprite OnSprite { get; private set; }
        public Sprite OffSprite { get; private set; }
        private bool shouldInvoke = true;
        public QMCFuncToggle(Transform parent, string text, System.Action<bool> listener, string tooltip = "", bool rightContainer = false, bool defaultState = false, bool separator = false, Sprite onSprite = null, Sprite offSprite = null)
        {
            if (!APIBase.IsReady())
                throw new NullReferenceException("Object Search has FAILED!");

            (transform = (gameObject = Object.Instantiate(ApiPaths.QMCarouselFuncButtonTemplate, parent)).transform).name = $"{text}_ToggleControlContainer";

            QMUtils.DestroyChildren((leftPar = transform.Find("LeftItemContainer")).gameObject);
            QMUtils.DestroyChildren((rightPar = transform.Find("RightItemContainer")).gameObject);

            ButtonParent = rightContainer ? rightPar : leftPar;

            transform.Find("TitleMainContainer").gameObject.SetActive(false);

            Transform newToggle = Object.Instantiate(ApiPaths.QMCarouselFuncButtonTemplate.transform.Find("LeftItemContainer/Button (1)"), ButtonParent);
            newToggle.name = text + "_FunctionToggle";

            (OnImageObj = Object.Instantiate(newToggle.Find("Icon").gameObject, newToggle)).name = "OnIcon";
            OnImageObj.GetComponent<Image>().sprite = onSprite ?? Icons.check_sprite;

            (OffImageObj = Object.Instantiate(newToggle.Find("Icon").gameObject, newToggle)).name = "OffIcon";
            OffImageObj.GetComponent<Image>().sprite = offSprite ?? Icons.cancel_sprite;

            newToggle.Find("Icon").gameObject.SetActive(false);

            TMProCompnt = newToggle.Find("Text_MM_H3").GetComponent<TextMeshProUGUI>();
            TMProCompnt.text = text;
            TMProCompnt.richText = true;

            if (separator != false)
                AddSeparator(parent);

            newToggle.GetComponent<ToolTip>()._localizableString = VRChatMenuExtensions.Localize(tooltip);

            bool isToggledLocal = defaultState;
            OnImageObj.SetActive(isToggledLocal);
            OffImageObj.SetActive(!isToggledLocal);

            Button buttonComponent = newToggle.GetComponent<Button>();
            buttonComponent.onClick = new Button.ButtonClickedEvent();
            buttonComponent.onClick.AddListener(new Action(() => {
                isToggledLocal = !isToggledLocal;

                OnImageObj.SetActive(isToggledLocal);
                OffImageObj.SetActive(!isToggledLocal);

                if (shouldInvoke)
                {
                    APIBase.SafelyInvoke(isToggledLocal, listener, text);
                }
            }));

            newToggle.gameObject.SetActive(true);

        }
        public QMCFuncToggle AddButton(string text, string tooltip, Action listener, bool rightContainer = false, Sprite sprite = null)
        {
            ButtonParent = rightContainer ? rightPar : leftPar;

            Transform newButton = Object.Instantiate(ApiPaths.QMCarouselFuncButtonTemplate.transform.Find("LeftItemContainer/Button (1)"), ButtonParent);
            newButton.name = text;

            if (sprite != null)
            {
                var Tsprite = newButton.Find("Icon").GetComponent<Image>();
                Tsprite.overrideSprite = sprite;
                Tsprite.gameObject.SetActive(true);
            }

            TMProCompnt = newButton.Find("Text_MM_H3").GetComponent<TextMeshProUGUI>();
            TMProCompnt.text = text;
            TMProCompnt.richText = true;

            newButton.GetComponent<ToolTip>()._localizableString = LocalizableStringExtensions.Localize(tooltip);

            ButtonCompnt = newButton.GetComponent<Button>();
            ButtonCompnt.onClick = new Button.ButtonClickedEvent();
            ButtonCompnt.onClick.AddListener(listener);

            newButton.gameObject.SetActive(true);

            return this;
        }
        public QMCFuncToggle AddToggle(string text, System.Action<bool> listener, string tooltip = "", bool rightContainer = false, bool defaultState = false, Sprite onSprite = null, Sprite offSprite = null)
        {
            ButtonParent = rightContainer ? rightPar : leftPar;

            Transform newToggle = Object.Instantiate(ApiPaths.QMCarouselFuncButtonTemplate.transform.Find("LeftItemContainer/Button (1)"), ButtonParent);
            newToggle.name = text + "_FunctionToggle";

            GameObject OnIconObj = Object.Instantiate(newToggle.Find("Icon").gameObject, newToggle);
            OnIconObj.name = "OnIcon";
            OnIconObj.GetComponent<Image>().sprite = onSprite ?? Icons.check_sprite;

            GameObject OffIconObj = Object.Instantiate(newToggle.Find("Icon").gameObject, newToggle);
            OffIconObj.name = "OffIcon";
            OffIconObj.GetComponent<Image>().sprite = offSprite ?? Icons.cancel_sprite;

            newToggle.Find("Icon").gameObject.SetActive(false);

            TMProCompnt = newToggle.Find("Text_MM_H3").GetComponent<TextMeshProUGUI>();
            TMProCompnt.text = text;
            TMProCompnt.richText = true;

            newToggle.GetComponent<ToolTip>()._localizableString = LocalizableStringExtensions.Localize(tooltip);

            bool isToggledLocal = defaultState;
            OnIconObj.SetActive(isToggledLocal);
            OffIconObj.SetActive(!isToggledLocal);

            Button buttonComponent = newToggle.GetComponent<Button>();
            buttonComponent.onClick = new Button.ButtonClickedEvent();
            buttonComponent.onClick.AddListener(new Action(() => {
                isToggledLocal = !isToggledLocal;
                OnIconObj.SetActive(isToggledLocal);
                OffIconObj.SetActive(!isToggledLocal);

                if (shouldInvoke)
                    APIBase.SafelyInvoke(isToggledLocal, listener, text);
            }));

            newToggle.gameObject.SetActive(true);

            return this;
        }

        public void SoftSetState(bool state)
        {
            if (isToggled != state)
            {
                isToggled = state;
                ToggleSprite.overrideSprite = isToggled ? OnSprite : OffSprite;

                if (shouldInvoke)
                    APIBase.SafelyInvoke(isToggled, Listener, "SoftSet");
            }
        }
        public QMCFuncToggle(QMCGroup group, string text, System.Action<bool> listener, string tooltip = "", bool rightContainer = false, bool defaultState = false, bool separator = false, Sprite onSprite = null, Sprite offSprite = null)
            : this(group.GetTransform().Find("QM_Settings_Panel/VerticalLayoutGroup").transform, text, listener, tooltip, rightContainer, defaultState, separator, onSprite, offSprite) { }
        public QMCFuncToggle(CollapsibleButtonGroup buttonGroup, string text, System.Action<bool> listener, string tooltip = "", bool rightContainer = false, bool defaultState = false, bool separator = false, Sprite onSprite = null, Sprite offSprite = null)
            : this(buttonGroup.QMCParent, text, listener, tooltip, rightContainer, defaultState, separator, onSprite, offSprite) { }
    }
}

