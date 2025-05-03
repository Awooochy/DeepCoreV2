namespace AstroClient.MenuApi.QM.Controls
{
    using System;
    using AstroMonos.Components.Tools.Listeners;
    using Buttons;
    using ClientResources.Helpers;
    using UnityEngine;
    using UnityEngine.UI;
    using VRC.UI;
    using VRC.UI.Core.Styles;
    using xAstroBoy.Utility;

    public class ExtendedControl : Root
    {
        public Button ButtonCompnt { get; internal set; }
        public Image ImgCompnt { get; internal set; }
        public Image BackgroundImgCompnt { get; internal set; }

        public Action onClickAction { get; set; }
        public string ToolTip { get; internal set; }

        internal VRCButton inst;

        public void SetSprite(Sprite sprite) => ImgCompnt.overrideSprite = sprite;
        public Sprite GetSprite() => ImgCompnt.sprite;
        public void ShowSubMenuIcon(bool state) => gameObject.transform.Find("Badge_MMJump").gameObject.SetActive(state);
        public void SetIconColor(Color color) => ImgCompnt.color = color;

        public override string SetToolTip(string tip)
        {
            ToolTip = tip;
            return base.SetToolTip(tip);
        }

        public void SetAction(Action newAction) => SetAction((_) => newAction());

        public void SetAction(Action<VRCButton> newAction)
        {
            ButtonCompnt.onClick = new Button.ButtonClickedEvent();
            onClickAction = () => newAction.Invoke(inst);
            ButtonCompnt.onClick.AddListener(new Action(() => APIBase.SafelyInvoke(onClickAction, Text)));
        }

        public void SetBackgroundImage(Sprite newImg)
        {
            if (BackgroundImgCompnt == null)
            {
                gameObject.RemoveComponent<StyleOverride>();
                return;
            }

            gameObject.GetOrAddComponent<StyleOverride>();
            BackgroundImgCompnt.sprite = newImg;
            BackgroundImgCompnt.color = Color.white; // RGBA = (1,1,1,1)
        }

        public void SetBackgroundImage(Texture2D newImg)
        {
            SetBackgroundImage(newImg.ToSprite());
        }

        public void SetBackgroundColor(Color newColor)
        {
            if (BackgroundImgCompnt == null) return;
            gameObject.GetOrAddComponent<StyleOverride>();
            BackgroundImgCompnt.color = newColor;
        }


        public void SetInteractable(bool state)
        {
            ButtonCompnt.interactable = state;
        }

        internal void ResetTextPox() => TMProCompnt.transform.localPosition = new Vector3(0, 0, 0);

        public void TurnHalf(ButtonHalfType Type, bool IsGroup)
        {
            ImgCompnt.gameObject.active = false;
            TMProCompnt.fontSize = 22;

            var Jmp = gameObject.transform.Find("Badge_MMJump");
            Jmp.localPosition = new Vector3(Jmp.localPosition.x, Jmp.localPosition.y - 34, Jmp.localPosition.z);

            if (IsGroup) gameObject.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -115);
            else gameObject.transform.Find("Background").GetComponent<RectTransform>().sizeDelta = new Vector2(0, -80);

            switch (Type)
            {
                case ButtonHalfType.Top:
                    ImgCompnt.transform.localPosition = new Vector3(0f, 0f, 0f);
                    TMProCompnt.transform.localPosition = new Vector3(0, 90, 0);
                    gameObject.transform.Find("Background").localPosition = new Vector3(0, 53, 0);
                    break;
                case ButtonHalfType.Normal:
                    ImgCompnt.transform.localPosition = new Vector3(0f, 0f, 0f);
                    TMProCompnt.transform.localPosition = new Vector3(0, 43, 0);
                    break;
                case ButtonHalfType.Bottom:
                    ImgCompnt.transform.localPosition = new Vector3(0f, 0f, 0f);
                    TMProCompnt.transform.localPosition = new Vector3(0, -5, 0);
                    gameObject.transform.Find("Background").localPosition = new Vector3(0, -53, 0);
                    break;
            }

        }

        public enum ButtonHalfType
        {
            Top,
            Normal,
            Bottom
        }
    }

}

