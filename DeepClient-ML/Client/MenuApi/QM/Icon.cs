namespace AstroClient.MenuApi.QM
{
    using System;
    using Buttons.Groups;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using VRC.Localization;
    using VRC.UI;
    using VRC.UI.Elements.Controls;
    using AstroClient.MenuApi.Tools;
    using ClientUI.QuickMenuGUI;
    using xAstroBoy;
    using Object = UnityEngine.Object;

    public class SDebugBar
    {
        public SDebugBar(string text)
        {
            Transform transform = ApiPaths.Canvas_QuickMenu.transform.FindObject("CanvasGroup/Container/Window/Panel_QM_Widget/Panel_QM_DebugInfo");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, transform.parent).gameObject;

            gameObject.SetActive(true);

            Object.Destroy(gameObject.transform.Find("Panel/Text_Ping"));

            gameObject.transform.Find("Panel/Text_FPS").GetComponent<TextMeshProUGUIEx>().text = text;
        }
    }

    public class UIText
    {
        public UIText(GameObject parent, string Message, Vector2 position, int size, bool wordWrap = false, TextAlignmentOptions textAnchor = TextAlignmentOptions.TopLeft)
        {
            this.CreateText(parent, Message, position, size, wordWrap, textAnchor);
        }

        public void SetText(string Message)
        {
            this.text.text = Message;
        }

        private void CreateText(GameObject parent, string Message, Vector2 position, int textsize, bool wordWrap, TextAlignmentOptions textAnchor)
        {
            this.TextObject = Object.Instantiate<GameObject>(ApiPaths.Canvas_QuickMenu.transform.FindObject("CanvasGroup/Container/Window/Panel_QM_Widget/Panel_QM_DebugInfo/Panel/Text_Ping").gameObject, parent.transform);
            foreach (Behaviour behaviour in this.TextObject.GetComponents<Behaviour>())
            {
                if (behaviour != this.TextObject.GetComponent<TextMeshProUGUI>())
                {
                    Object.DestroyImmediate(behaviour);
                }
            }
            this.TextObject.name = "Test" + Guid.NewGuid();
            this.text = this.TextObject.GetComponent<TextMeshProUGUI>();
            this.text.name = this.TextObject.name;
            this.text.text = Message;
            this.text.richText = true;
            this.text.fontStyle = FontStyles.Bold;
            this.text.enableWordWrapping = wordWrap;
            this.text.autoSizeTextContainer = false;
            this.text.fontSize = (float)textsize;
            this.text.fontSizeMax = (float)textsize;
            this.text.fontSizeMin = (float)textsize;
            this.text.alignment = textAnchor;
            this.text.enableKerning = false;
            this.TextObject.GetComponent<RectTransform>().localPosition = position;
            this.TextObject.GetComponent<RectTransform>().sizeDelta = new Vector2(600f, 0f);
        }

        public TextMeshProUGUI text;

        public GameObject TextObject;

        internal static UIText Instance;
    }

    public class SInfoBar
    {
        public SInfoBar(string text, System.Action action)
        {
            Transform transform = ApiPaths.Canvas_QuickMenu.transform.FindObject("CanvasGroup/Container/Window/Panel_QM_Widget/Header_StreamerMode");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, transform.parent).gameObject;

            gameObject.transform.Find("Header/LeftItemContainer/Text_Title").GetComponent<TextMeshProUGUIEx>().text = text;
        }
    }

    public class SIconButtonAction
    {
        public SIconButtonAction(string toolTip, System.Action action, Sprite icon)
        {
            Transform transform = ApiPaths.Canvas_QuickMenu.transform.FindObject("CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/Header_H1/LeftItemContainer/Button_Back");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, transform.parent).gameObject;
            gameObject.transform.Find("Icon").GetComponent<Image>().overrideSprite = icon;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.m_OnClick.RemoveAllListeners();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(action));
        }
    }

    public class SAvatarButton
    {
        public SAvatarButton(string text, string toolTip, System.Action action, Sprite icon)
        {
            Transform transform = ApiPaths.Canvas_MainMenu.transform.FindObject("Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Panel_SelectedAvatar/ScrollRect/Viewport/VerticalLayoutGroup/Button_AvatarDetails");
            UnityEngine.Object.DestroyImmediate(transform.parent.Find("Button_ResetHeight"));
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, transform.parent).gameObject;
            gameObject.transform.Find("Text_ButtonName").GetComponent<TextMeshProUGUIEx>().text = text;
            gameObject.transform.Find("Text_ButtonName/Icon").GetComponent<Image>().overrideSprite = icon;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(action));
        }
    }

    public class SArticleButton
    {
        public SArticleButton(VRCPage menu, string toolTip, System.Action action, Sprite icon)
        {
            Transform transform = ApiPaths.Canvas_MainMenu.transform.FindObject("Container/MMParent/Menu_MM_Help&Info/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/CellGrid_MM_ArticlePage_3Column/Cell_MM_Article");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, menu.transform).gameObject;
            gameObject.transform.Find("Image_Mask/ContentImage").GetComponent<RawImageEx>().texture = icon.texture;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(action));
        }
    }

    public class SIconButtonTarget
    {
        public SIconButtonTarget(VRCPage menu, string toolTip, Sprite icon)
        {
            Transform transform = ApiPaths.Canvas_QuickMenu.transform.FindObject("CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/Header_H1/LeftItemContainer/Button_Back");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, transform.parent).gameObject;
            gameObject.transform.Find("Icon").GetComponent<Image>().overrideSprite = icon;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(menu.OpenMenu));
        }
    }

    public class SpriteButton
    {
        public SpriteButton(Transform newLocation, string text, string toolTip, Action action, Sprite Sprite)
        {
            Transform transform = ApiPaths.Canvas_QuickMenu.transform.FindObject("CanvasGroup/Container/Window/QMParent/Menu_QM_Emojis/ScrollRect/Viewport/VerticalLayoutGroup/QM_EmojiGrid(Clone)/Button_Wing_Emoji(Clone)");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, newLocation).gameObject;
            //Widget.transform.Find("Text").GetComponent<Text>().text = text;
            gameObject.transform.Find("Container/Icon").GetComponent<Image>().overrideSprite = Sprite;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(action));
        }
    }

    public class SIconButton
    {
        public SIconButton(Transform transform2, Action action, string toolTip, Sprite icon)
        {
            Transform transform = ApiPaths.Canvas_QuickMenu.transform.FindObject(transform2.ToString());
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, transform.parent).gameObject;
            gameObject.transform.Find("Icon").GetComponent<Image>().overrideSprite = icon;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(action));
        }

        public SIconButton(VRCPage menu, string toolTip, Sprite icon)
        {
            Transform transform = ApiPaths.Canvas_QuickMenu.transform.FindObject("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer/Button_QM_Expand");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, transform.parent).gameObject;
            gameObject.transform.Find("Icon").GetComponent<Image>().overrideSprite = icon;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(menu.OpenMenu));
        }

        public SIconButton(Action action, string toolTip, Sprite icon)
        {
            Transform transform = ApiPaths.Canvas_QuickMenu.transform.FindObject("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer/Button_QM_Expand");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, transform.parent).gameObject;
            gameObject.transform.Find("Icon").GetComponent<Image>().overrideSprite = icon;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(action));
        }

        public SIconButton(Vector3 position, Action action, string toolTip, Sprite icon)
        {
            Transform transform = ApiPaths.Canvas_QuickMenu.transform.FindObject("CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer/Button_QM_Expand");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, GameObject.Find("UserInterface/Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window").transform).gameObject;
            gameObject.transform.Find("Icon").GetComponent<Image>().overrideSprite = icon;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(action));
            gameObject.transform.localPosition = position;
        }
    }

    public class SWingLeftButton
    {
        public SWingLeftButton(string text, string toolTip, Action action, Sprite icon)
        {
            Transform transform = ApiPaths.Canvas_QuickMenu.transform.FindObject("CanvasGroup/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emoji/");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, transform.parent).gameObject;
            gameObject.transform.Find("Container/Icon").GetComponent<Image>().overrideSprite = icon;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            gameObject.transform.Find("Container/Text_QM_H3").GetComponent<TextMeshProUGUIEx>().text = text;
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(action));
        }
    }

    public class SWingRightButton
    {
        public SWingRightButton(string text, string toolTip, Action action, Sprite icon)
        {
            Transform transform = ApiPaths.Canvas_QuickMenu.transform.FindObject("CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup/Button_Emoji/");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, transform.parent).gameObject;
            gameObject.transform.Find("Container/Icon").GetComponent<Image>().overrideSprite = icon;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            gameObject.transform.Find("Container/Text_QM_H3").GetComponent<TextMeshProUGUIEx>().text = text;
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(action));
        }
    }

    public class SWorldButton
    {
        public SWorldButton(string Text, string toolTip, Action action, Sprite icon)
        {
            Transform transform = ApiPaths.Canvas_MainMenu.transform.FindObject("Container/MMParent/Menu_MM_WorldInformation/Panel_World_Information/Content/Viewport/BodyContainer_World_Details/ScrollRect/Viewport/VerticalLayoutGroup/Actions/ViewOnWebsite");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, transform.parent).gameObject;
            gameObject.transform.Find("Text_ButtonName/Icon").GetComponent<Image>().overrideSprite = icon;
            gameObject.transform.Find("Text_ButtonName").GetComponent<TextMeshProUGUIEx>().text = Text;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(action));
        }
    }

    public class SUserButton
    {
        public SUserButton(string Text, string toolTip, Action action, Sprite icon)
        {
            Transform transform = ApiPaths.Canvas_MainMenu.transform.FindObject("Container/MMParent/Menu_UserDetail/ScrollRect/Viewport/VerticalLayoutGroup/Row3/CellGrid_MM_Content/AddANote");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, transform.parent).gameObject;
            gameObject.transform.Find("Text_ButtonName/Icon").GetComponent<Image>().overrideSprite = icon;
            gameObject.transform.Find("Text_ButtonName").GetComponent<TextMeshProUGUIEx>().text = Text;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(action));
        }
    }

    public class SHighlightButton
    {
        public SHighlightButton(Transform settransform, string Text, string toolTip, Action action)
        {
            Transform transform = ApiPaths.Canvas_MainMenu.transform.FindObject("Container/MMParent/Menu_Search/ScrollRect/Viewport/VerticalLayoutGroup/CellGrid_MM_QuickSearch/Cell_MM_CannedSearch");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, settransform.parent).gameObject;
            gameObject.transform.Find("DetailBackground/Text_MM_H1").GetComponent<TextMeshProUGUIEx>().text = Text;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(action));
        }

        public SHighlightButton(CollapsibleButtonGroup settransform, string Text, string toolTip, Action action)
        {
            Transform transform = ApiPaths.Canvas_MainMenu.transform.FindObject("Container/MMParent/Menu_Search/ScrollRect/Viewport/VerticalLayoutGroup/CellGrid_MM_QuickSearch/Cell_MM_CannedSearch");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, settransform.transform).gameObject;
            gameObject.transform.Find("DetailBackground/Text_MM_H1").GetComponent<TextMeshProUGUIEx>().text = Text;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(action));
        }

        public SHighlightButton(ButtonGroup settransform, string Text, string toolTip, Action action)
        {
            Transform transform = ApiPaths.Canvas_MainMenu.transform.FindObject("Container/MMParent/Menu_Search/ScrollRect/Viewport/VerticalLayoutGroup/CellGrid_MM_QuickSearch/Cell_MM_CannedSearch");
            GameObject gameObject = UnityEngine.Object.Instantiate<Transform>(transform, settransform.transform).gameObject;
            gameObject.transform.Find("DetailBackground/Text_MM_H1").GetComponent<TextMeshProUGUIEx>().text = Text;
            gameObject.GetComponent<ToolTip>()._localizableString = toolTip.Localize();
            VRCButtonHandle component = gameObject.GetComponent<VRCButtonHandle>();
            component.onClick.RemoveAllListeners();
            component.onClick.AddListener(new Action(action));
        }
    }
}