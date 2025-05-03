namespace AstroClient.MenuApi.QM.Buttons.Groups
{
    using System;
    using ClientUI.QuickMenuGUI;
    using Controls;
    using Extras;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using Object = UnityEngine.Object;

    public class ButtonGroup : ButtonGroupControl
    {
        private readonly GridLayoutGroup Layout;

        public ButtonGroup(Transform parent, string text, bool NoText = false, TextAnchor ButtonAlignment = TextAnchor.UpperCenter)
        {
            if (!APIBase.IsReady())
                throw new NullReferenceException("Object Search has FAILED!");

            if (!(WasNoText = NoText))
            {
                TMProCompnt = (headerGameObject = Object.Instantiate(ApiPaths.ButtonGrpText, parent)).GetComponentInChildren<TextMeshProUGUI>(true);
                headerGameObject.GetComponentInChildren<TextMeshProUGUI>().text = text;
                Text = TMProCompnt.text = text;
                TMProCompnt.richText = true;
            }

            (transform = (GroupContents = gameObject = Object.Instantiate(ApiPaths.ButtonGrp, parent)).transform).name = text;
            transform.DestroyChildren();

            Layout = gameObject.GetComponent<GridLayoutGroup>();
            Layout.childAlignment = ButtonAlignment;

            parentMenuMask = parent.parent.GetComponent<RectMask2D>();

        }

        public void ChangeChildAlignment(TextAnchor ButtonAlignment = TextAnchor.UpperCenter) => Layout.childAlignment = ButtonAlignment;

        public ButtonGroup(ClientPage page, string text, bool NoText = false, TextAnchor ButtonAlignment = TextAnchor.UpperCenter) : this(page.MenuContents, text, NoText, ButtonAlignment) { }

    }


}

