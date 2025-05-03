namespace AstroClient.MenuApi.QM
{
    using System;
    using AstroClient.ClientUI.QuickMenuGUI;
    using Controls;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using VRC.UI.Core.Styles;
    using VRC.UI.Elements.Controls;
    using Object = UnityEngine.Object;

    public class Tab : ExtendedControl
    {
        public GameObject badgeGameObject { get; private set; }
        public TextMeshProUGUI badgeText { get; private set; }
        public Image tabIcon { get; private set; }
        public Button Button { get; private set; }

        public VRCPage Menu { get; private set; }

        private StyleElement element;

        public Tab(VRCPage menu, string tooltip, Sprite icon = null, Transform parent = null, bool openMenu = true)
        {
            if (!APIBase.IsReady()) throw new Exception();

            if (parent == null)
                parent = ApiPaths.Tab.transform.parent;
            Menu = menu;

            (gameObject = UnityEngine.Object.Instantiate(ApiPaths.Tab, parent)).name = $"{menu.MenuName}_Tab";
            (tabIcon = gameObject.transform.Find("Icon").GetComponent<Image>()).overrideSprite = icon;
            (element = gameObject.GetComponent<StyleElement>()).field_Private_Selectable_0 = (Button = gameObject.GetComponent<Button>());
            (Button.onClick = new Button.ButtonClickedEvent()).AddListener((Action)delegate {
                gameObject.GetComponent<StyleElement>().field_Private_Selectable_0 = Button;
                if (openMenu) Menu.OpenMenu();
                onClickAction.Invoke();
            });
            Button.GetComponent<MenuTab>()._controlName = menu.MenuName;

            badgeGameObject = gameObject.transform.GetChild(0).gameObject;
            badgeText = badgeGameObject.GetComponentInChildren<TextMeshProUGUI>();

            SetToolTip(tooltip);

            gameObject.active = true;
        }
    }
}

