namespace AstroClient.MenuApi.QM.Controls
{
    using TMPro;
    using UnityEngine;

    public class SliderControl : QMCControl
    {
        public TextMeshProUGUI TextMeshPro { get; set; }
        public Transform body { get; set; }
        public Transform valDisplay { get; set; }
        public Transform slider { get; set; }
        public float DefaultValue { get; set; }
        public SnapSliderExtendedCallbacks snapSlider { get; set; }

        public System.Action<bool> ToggleListener { get; set; }
        public bool ToggleValue { get; set; }

        public Transform ResetButton { get; set; }
    }
}

