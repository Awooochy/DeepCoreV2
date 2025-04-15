using UnityEngine;
using VRC;

namespace DeepCore.Client.Module.Visual
{
    internal class Flashlight
    {
        public static Light light;
        public static int intensity = 1;
        public static int range = 60;

        public static void State(bool s)
        {
            var LocalPlayer = Player.prop_Player_0;
            if (s)
            {
                if (LocalPlayer == null)
                    return;
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
