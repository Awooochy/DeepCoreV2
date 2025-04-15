using UnityEngine;

namespace DeepClient.Client.Module.Movement
{
    internal class PosSaver
    {
        public static Vector3 targetPos;
        public static Quaternion targetRotation;
        public static bool IsEnabled = false;
        public static void State(bool s)
        {
            var Localplayer = VRCPlayer.field_Internal_Static_MonoBehaviour1PublicOb_pOb_s_pBoGaOb_pStUnique_0;
            if (s)
            {
                if (Localplayer == null)
                    return;
                targetPos = Localplayer.gameObject.transform.position;
                targetRotation = Localplayer.gameObject.transform.rotation;
            }
            else
            {
                if (Localplayer == null)
                    return;
                Localplayer.gameObject.transform.position = targetPos;
                Localplayer.gameObject.transform.rotation = targetRotation;
            }
        }
    }
}