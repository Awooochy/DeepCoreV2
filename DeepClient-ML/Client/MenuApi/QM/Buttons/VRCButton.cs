namespace AstroClient.MenuApi.QM.Buttons
{
    using System;
    using AstroClient.ClientUI.QuickMenuGUI;
    using ClientResources.Loaders;
    using Controls;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using VRC.UI;
    using VRC.UI.Core.Styles;
    using xAstroBoy;
    using xAstroBoy.Utility;
    using Object = UnityEngine.Object;

    public class VRCButton : ExtendedControl
    {
        public VRCButton(Transform menu, string text, string tooltip, Action<VRCButton> listener, bool Half = false,
            bool SubMenuIcon = false, Sprite Icon = null, ButtonHalfType Type = ButtonHalfType.Normal, bool IsGroup = false)
        {
            if (!APIBase.IsReady())
                throw new NullReferenceException("Object Search has FAILED!");

            if(ApiPaths.Button_NewInstance == null)
                throw new NullReferenceException("Unable to grab Button Prefab");


            (transform = (gameObject = Object.Instantiate(ApiPaths.Button_NewInstance, menu).gameObject).transform).name = $"Button_{text}";
            gameObject.SetActive(true);

            TMProCompnt = transform.GetComponentInChildren<TextMeshProUGUI>();
            if (TMProCompnt == null)
                throw new NullReferenceException("Unable to grab Text Object");

            Text = TMProCompnt.text = text;
            TMProCompnt.richText = true;
            TMProCompnt.RemoveComponents<StyleElement>();

            ButtonCompnt = transform.GetComponent<Button>();
            if (ButtonCompnt == null)
                throw new NullReferenceException("Unable to grab Button Componet");

            ButtonCompnt.onClick = new Button.ButtonClickedEvent();
            if (listener != null) SetAction(listener);
            else ButtonCompnt.interactable = false;


            ImgCompnt = transform.FindObject("Icons/Icon").GetComponent<Image>();
            ImgCompnt.RemoveComponents<StyleElement>();

            BackgroundImgCompnt = transform.FindObject("Background").GetComponent<Image>();

            //var elemetn = ImgCompnt.gameObject.GetComponent<StyleElement>();
            //if (elemetn != null) elemetn.enabled = false; Set as off to fix styling

            if (ImgCompnt.color == Color.black)
                ImgCompnt.color = Color.white;

            Object.Destroy(transform.FindObject("Icons/Icon_Secondary").gameObject);
            if (Icon != null)
                SetSprite(Icon);
            else
            {
                transform.FindObject("Icons/Icon").gameObject.active = false;
                ResetTextPox();
            }

            ShowSubMenuIcon(SubMenuIcon);
            this.SetToolTip(tooltip);
            this.SetTextColor(Color.white);
            if (Half) TurnHalf(Type, IsGroup);
            inst = this;

        }

        public VRCButton(Transform menu, string text, string tooltip, Action click, bool Half = false, bool subMenuIcon = false, Sprite icon = null, ButtonHalfType Type = ButtonHalfType.Normal, bool IsGroup = false)
            : this(menu, text, tooltip, (_) => click(), Half, subMenuIcon, icon, Type, IsGroup) { }

        public VRCButton(ButtonGroupControl buttonGroup, string text, string tooltip, Action click, bool Half = false, bool subMenuIcon = false, Sprite icon = null, ButtonHalfType Type = ButtonHalfType.Normal, bool IsGroup = false)
        : this(buttonGroup, text, tooltip, (_) => click(), Half, subMenuIcon, icon, Type, IsGroup)
        {
            if (!Half && icon == null)
                transform.Find("Icons").gameObject.active = false;
        }

        public VRCButton(ButtonGroupControl buttonGroup, string text, string tooltip, Action<VRCButton> click, bool Half = false, bool subMenuIcon = false, Sprite icon = null, ButtonHalfType Type = ButtonHalfType.Normal, bool IsGroup = false)
            : this(buttonGroup.GroupContents.transform, text, tooltip, click, Half, subMenuIcon, icon, Type, IsGroup) => buttonGroup._buttons.Add(this);

    }

}

