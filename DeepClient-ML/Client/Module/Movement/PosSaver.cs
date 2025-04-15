using UnityEngine;
using VRC;

namespace DeepCore.Client.Module.Movement
{
    internal class PosSaver
    {
        public static Vector3 targetPos;
        public static Quaternion targetRotation;
        public static void State(bool s)
        {
            var Localplayer = Player.prop_Player_0;
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
