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
    using VRC.UI.Core.Styles;
    using VRC.UI.Elements.Controls;
    using xAstroBoy.Utility;
    using Object = UnityEngine.Object;

    public class QMCFuncButton : QMCControl
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
        private Image Tsprite;
        public QMCFuncButton(Transform parent, string text, string tooltip, Action listener, bool rightContainer = false, bool separator = false, Sprite sprite = null)
        {
            if (!APIBase.IsReady())
                throw new NullReferenceException("Object Search has FAILED!");

            (transform = (gameObject = Object.Instantiate(ApiPaths.QMCarouselFuncButtonTemplate, parent)).transform).name = $"{text}_ControlContainer";

            QMUtils.DestroyChildren((leftPar = transform.Find("LeftItemContainer")).gameObject);
            QMUtils.DestroyChildren((rightPar = transform.Find("RightItemContainer")).gameObject);

            transform.RemoveComponents<StyleElement>();
            ButtonParent = rightContainer ? rightPar : leftPar;

            transform.Find("TitleMainContainer").gameObject.SetActive(false);

            Transform button = Object.Instantiate(ApiPaths.QMCarouselFuncButtonTemplate.transform.Find("LeftItemContainer/Button (1)"), ButtonParent);
            button.name = text;

            (TMProCompnt = button.Find("Text_MM_H3").GetComponent<TextMeshProUGUI>()).text = text;
            TMProCompnt.richText = true;

            if (sprite != null)
            {
                (Tsprite = button.Find("Icon").GetComponent<Image>()).overrideSprite = sprite;
                Tsprite.gameObject.SetActive(true);
            }

            if (separator != false)
                AddSeparator(parent);

            button.GetComponent<ToolTip>()._localizableString = VRChatMenuExtensions.Localize(tooltip);

            (ButtonCompnt = button.GetComponent<Button>()).onClick = new Button.ButtonClickedEvent();
            ButtonCompnt.onClick.AddListener(listener);

            button.gameObject.SetActive(true);
        }
        public QMCFuncButton AddButton(string text, string tooltip, Action listener, bool rightContainer = false, Sprite sprite = null)
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

            (TMProCompnt = newButton.Find("Text_MM_H3").GetComponent<TextMeshProUGUI>()).text = text;
            TMProCompnt.richText = true;

            newButton.GetComponent<ToolTip>()._localizableString = LocalizableStringExtensions.Localize(tooltip);

            (ButtonCompnt = newButton.GetComponent<Button>()).onClick = new Button.ButtonClickedEvent();
            ButtonCompnt.onClick.AddListener(listener);

            newButton.gameObject.SetActive(true);

            return this;
        }
        public QMCFuncButton AddToggle(string text, System.Action<bool> listener, string tooltip = "", bool rightContainer = false, bool defaultState = false, Sprite onSprite = null, Sprite offSprite = null)
        {
            ButtonParent = rightContainer ? rightPar : leftPar;

            Transform newToggle = Object.Instantiate(ApiPaths.QMCarouselFuncButtonTemplate.transform.Find("LeftItemContainer/Button (1)"), ButtonParent);
            newToggle.name = $"{text}_FunctionToggle";

            (OnImageObj = Object.Instantiate(newToggle.Find("Icon").gameObject, newToggle)).name = "OnIcon";
            OnImageObj.GetComponent<Image>().sprite = onSprite ?? Icons.check_sprite;

            (OffImageObj = Object.Instantiate(newToggle.Find("Icon").gameObject, newToggle)).name = "OffIcon";
            OffImageObj.GetComponent<Image>().sprite = offSprite ?? Icons.cancel_sprite;

            newToggle.Find("Icon").gameObject.SetActive(false);

            (TMProCompnt = newToggle.Find("Text_MM_H3").GetComponent<TextMeshProUGUI>()).text = text;
            TMProCompnt.richText = true;

            newToggle.GetComponent<ToolTip>()._localizableString = LocalizableStringExtensions.Localize(tooltip);

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

        public QMCFuncButton(QMCGroup group, string text, string tooltip, Action listener, bool rightContainer = false, bool separator = false, Sprite sprite = null)
            : this(group.GetTransform().Find("QM_Settings_Panel/VerticalLayoutGroup").transform, text, tooltip, listener, rightContainer, separator, sprite) { }
        public QMCFuncButton(CollapsibleButtonGroup buttonGroup, string text, string tooltip, Action listener, bool rightContainer = false, bool separator = false, Sprite sprite = null)
            : this(buttonGroup.QMCParent, text, tooltip, listener, rightContainer, separator, sprite) { }
    }

}

