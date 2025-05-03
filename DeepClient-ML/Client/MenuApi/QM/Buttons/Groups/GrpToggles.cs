namespace AstroClient.MenuApi.QM.Buttons.Groups
{
    using System;
    using Controls;
    using Extras;
    using UnityEngine;
    using UnityEngine.UI;

    public class GroupToggles : Root
    {
        public Transform ObjectHolder { get; private set; }

        public VRCToggle ToggleOne { get; private set; }
        public VRCToggle ToggleTwo { get; private set; }
        public VRCToggle ToggleThree { get; private set; }

        public GroupToggles(GameObject menu, string FirstName, string FirstOnTooltip, string FirstOffTooltip, System.Action<bool> FirstBoolStateChange,
            string SecondName = null, string SecondOnTooltip = null, string SecondOffTooltip = null, System.Action<bool> SecondBoolStateChange = null,
            string thirdName = null, string thirdOnTooltip = null, string thirdOffTooltip = null, System.Action<bool> thirdBoolStateChange = null,
            Sprite OnImageSprite = null, Sprite OffImageSprite = null,
            float FirstFontSize = 24f, float SecondFontSize = 24f, float ThirdFontSize = 24f,
            bool FirstState = false, bool SecondState = false, bool ThirdState = false)
        {
            if (!APIBase.IsReady())
                throw new NullReferenceException("Object Search had FAILED!");

            (transform = (gameObject = new GameObject("Toggle_GroupOfToggles")).transform).parent = menu.transform;
            QMUtils.ResetTransform(transform);
            gameObject.AddComponent<LayoutElement>();

            (ObjectHolder = new GameObject("Holder").transform).parent = gameObject.transform;
            QMUtils.ResetTransform(ObjectHolder);
            ObjectHolder.localPosition = new Vector3(0f, -3f, 0f);

            ToggleOne = new VRCToggle(ObjectHolder, FirstName, FirstBoolStateChange, FirstState, FirstOnTooltip, FirstOffTooltip, OnImageSprite, OffImageSprite)
                .TurnHalfReturn(new Vector3(0f, 10.7f, 0f), FirstFontSize);

            if (SecondBoolStateChange != null)
                ToggleTwo = new VRCToggle(ObjectHolder, SecondName, SecondBoolStateChange, SecondState, SecondOnTooltip, SecondOffTooltip, OnImageSprite, OffImageSprite)
                    .TurnHalfReturn(new Vector3(0f, -1.36f, 0f), SecondFontSize);

            if (thirdBoolStateChange != null)
                ToggleThree = new VRCToggle(ObjectHolder, thirdName, thirdBoolStateChange, ThirdState, thirdOnTooltip, thirdOffTooltip, OnImageSprite, OffImageSprite)
                    .TurnHalfReturn(new Vector3(0f, -13.88f, 0f), ThirdFontSize);
        }

        public GroupToggles(ButtonGroupControl grp, string FirstName, string FirstOnTooltip, string FirstOffTooltip, System.Action<bool> FirstBoolStateChange,
            string SecondName = null, string SecondOnTooltip = null, string SecondOffTooltip = null, System.Action<bool> SecondBoolStateChange = null,
            string thirdName = null, string thirdOnTooltip = null, string thirdOffTooltip = null, System.Action<bool> thirdBoolStateChange = null,
            Sprite OnImageSprite = null, Sprite OffImageSprite = null,
            float FirstFontSize = 24f, float SecondFontSize = 24f, float ThirdFontSize = 24f,
            bool FirstState = false, bool SecondState = false, bool ThirdState = false)
            : this(grp.GroupContents, FirstName, FirstOnTooltip, FirstOffTooltip, FirstBoolStateChange,
                SecondName, SecondOnTooltip, SecondOffTooltip, SecondBoolStateChange,
                thirdName, thirdOnTooltip, thirdOffTooltip, thirdBoolStateChange,
                OnImageSprite, OffImageSprite, FirstFontSize, SecondFontSize, ThirdFontSize, FirstState, SecondState, ThirdState)
        { }
    }
}