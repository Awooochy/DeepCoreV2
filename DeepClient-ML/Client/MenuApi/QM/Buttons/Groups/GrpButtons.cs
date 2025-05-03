using static AstroClient.MenuApi.QM.Controls.ExtendedControl;

namespace AstroClient.MenuApi.QM.Buttons.Groups{
    using System;
    using Controls;
    using Extras;
    using UnityEngine;
    using UnityEngine.UI;

    public class GrpButtons : Root
    {
        public Transform ObjectHolder { get; private set; }

        public VRCButton ButtonOne { get; private set; }
        public VRCButton ButtonTwo { get; private set; }
        public VRCButton ButtonThree { get; private set; }

        public GrpButtons(GameObject menu, string FirstName, string FirstTooltip, Action action,
            string SecondName = null, string SecondTooltip = null, Action Secondaction = null,
            string thirdName = null, string thirdTooltip = null, Action thirdaction = null)
        {
            (transform = (gameObject = new GameObject("Button_GroupOfHalfButtons")).transform).parent = menu.transform;
            QMUtils.ResetTransform(transform);
            gameObject.AddComponent<LayoutElement>();

            (ObjectHolder = new GameObject("Holder").transform).parent = gameObject.transform; // this has a reason!
            QMUtils.ResetTransform(ObjectHolder);
            ObjectHolder.localPosition = new Vector3(0f, -3f, 0f);

            ButtonOne = new VRCButton(ObjectHolder, FirstName, FirstTooltip, action, true, false, null, ButtonHalfType.Top, true);
            ButtonOne.transform.localPosition = new Vector3(0f, 10.7f, 0);

            if (Secondaction != null)
                (ButtonTwo = new VRCButton(ObjectHolder, SecondName, SecondTooltip, Secondaction, true, false, null, ButtonHalfType.Normal, true))
                    .transform.localPosition = new Vector3(0f, -1.36f, 0);

            if (thirdaction != null)
                (ButtonThree = new VRCButton(ObjectHolder, thirdName, thirdTooltip, thirdaction, true, false, null, ButtonHalfType.Bottom, true))
                    .transform.localPosition = new Vector3(0f, -13.88f, 0);
            SetTextColor(Color.white);
        }

        public GrpButtons(ButtonGroupControl grp, string FirstName, string FirstTooltip, Action action,
            string SecondName = null, string SecondTooltip = null, Action Secondaction = null,
            string thirdName = null, string thirdTooltip = null, Action thirdaction = null) : this(grp.GroupContents, FirstName, FirstTooltip, action,
                SecondName, SecondTooltip, Secondaction,
                thirdName, thirdTooltip, thirdaction)
        { }
    }

}

