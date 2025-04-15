using UnityEngine;
using VRC.Localization;

namespace DeepClient.Client.API.ButtonAPI.QM
{
    internal class BAPIUtils
    {
        public static void SetToolTipText(string simg, string text)
        {
            var components = GameObject.Find(simg).GetComponents<VRCBtnToolTip>();
            foreach (var component in components)
                component._localizableString = LocalizableStringExtensions.Localize(text);
        }
    }
}
