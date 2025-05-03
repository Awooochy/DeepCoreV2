namespace AstroClient.MenuApi.QM.Carousel.Items
{
    using System;
    using ClientUI.QuickMenuGUI;
    using Controls;
    using TMPro;
    using UnityEngine;
    using Object = UnityEngine.Object;

    public class QMCTitle : QMCControl
    {
        public QMCTitle(Transform parent, string text, bool separator = false)
        {
            if (!APIBase.IsReady())
                throw new NullReferenceException("Object Search has FAILED!");

            (transform = (gameObject = Object.Instantiate(ApiPaths.QMCarouselTitleTemplate, parent)).transform).name = text;

            (TMProCompnt = transform.Find("LeftItemContainer/Text_MM_H3").GetComponent<TextMeshProUGUI>()).text = text;
            TMProCompnt.richText = true;

            if (separator != false)
                AddSeparator(parent);
        }
        public QMCTitle(QMCGroup group, string text, bool separator = false)
            : this(group.GetTransform().Find("QM_Settings_Panel/VerticalLayoutGroup"), text, separator) { }
    }

}

