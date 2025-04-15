using UnityEngine;

namespace DeepClient.Client.Module.Visuals
{
    internal class Flashlight
    {
        public static Light light;
        public static int intensity = 1;
        public static int range = 60;
        public static bool IsEnabled = false;

        public static void State(bool s)
        {
            var LocalPlayer = VRCPlayer.field_Internal_Static_MonoBehaviour1PublicOb_pOb_s_pBoGaOb_pStUnique_0;
            if (s)
            {
                if (LocalPlayer == null)
                    return;
                IsEnabled = s;
                light = LocalPlayer.gameObject.AddComponent<Light>();
                light.type = LightType.Directional;
                light.intensity = intensity;
                light.range = range;
            }
            else
            {
                if (LocalPlayer == null)
                    return;
                if (light != null)
                {
                    MonoBehaviour.Destroy(light);
                }
            }
        }
    }
}