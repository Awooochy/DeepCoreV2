namespace AstroClient.MenuApi.QM.Controls
{
    using AstroClient.ClientUI.QuickMenuGUI;
    using UnityEngine;
    using UnityEngine.UI;
    using VRC.UI.Elements.Tooltips;
    using Object = UnityEngine.Object;

    public class QMCControl : ExtendedControl
    {
        public GameObject OnImageObj { get; internal set; }
        public GameObject OffImageObj { get; internal set; }
        public Toggle ToggleCompnt { get; internal set; }
        public Transform Handle { get; internal set; }
        public UiToggleTooltip ToggleToolTip { get; internal set; }

        private GameObject seB;
        public void AddSeparator(Transform p) => (seB = Object.Instantiate(ApiPaths.QMCarouselSeparator, p)).name = "Separator";
    }

}

