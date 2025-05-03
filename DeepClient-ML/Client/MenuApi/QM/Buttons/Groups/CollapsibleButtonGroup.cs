namespace AstroClient.MenuApi.QM.Buttons.Groups
{
    using System;
    using AstroClient.ClientUI.QuickMenuGUI;
    using Controls;
    using Extras;
    using UnityEngine;
    using UnityEngine.UI;
    using Object = UnityEngine.Object;

    public class CollapsibleButtonGroup : ButtonGroupControl
    {
        public bool IsOpen { get; internal set; }
        public GameObject headerObj { get; internal set; }
        public ButtonGroup buttonGroup { get; internal set; }
        public Transform QMCParent { get; set; }
        public System.Action<bool> OnClose { get; set; }

        public CollapsibleButtonGroup(Transform parent, string text, bool openByDefault = true)
        {
            if (!APIBase.IsReady())
                throw new NullReferenceException("Object Search has FAILED!");

            (headerObj = Object.Instantiate(ApiPaths.ColpButtonGrp, parent)).name = $"{text}_CollapsibleButtonGroup";
            headerObj.transform.Find("QM_Settings_Panel/VerticalLayoutGroup").DestroyChildren();

            QMCParent = headerObj.transform.Find("QM_Settings_Panel/VerticalLayoutGroup");

            TMProCompnt = headerObj.transform.Find("QM_Foldout/Label").GetComponent<TMPro.TextMeshProUGUI>();
            TMProCompnt.richText = true;
            TMProCompnt.text = text;

            gameObject = (buttonGroup = new ButtonGroup(parent, string.Empty, true)).gameObject;
            GroupContents = buttonGroup.GroupContents;

            OnClose = new System.Action<bool>((val) => {
                buttonGroup.gameObject.SetActive(val);
                IsOpen = val;
            });

            var foldout = headerObj.transform.Find("QM_Foldout/Background_Button").GetComponent<Toggle>();
            foldout.isOn = openByDefault;
            foldout.onValueChanged.AddListener(new System.Action<bool>(val => OnClose.Invoke(val)));

        }

        public CollapsibleButtonGroup(ClientPage page, string text, bool openByDefault = false) : this(page.MenuContents, text, openByDefault) { }
    }

}

