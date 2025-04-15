using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace DeepCore.Client.Module.QOL
{
    internal class QMFreeze
    {
        public static bool Frozen;
        private static Vector3 _originalGravity;
        private static Vector3 _originalVelocity;
        public static bool Enabled { get; set; } = true;
        public static bool RestoreVelocity { get; set; } = false;

        public static void State(bool s)
        {
            if (s)
            {
                _originalGravity = Physics.gravity;
                _originalVelocity = Player.prop_Player_0.field_Private_VRCPlayerApi_0.GetVelocity();
                if (_originalVelocity == Vector3.zero) return;
                Physics.gravity = Vector3.zero;
                Player.prop_Player_0.field_Private_VRCPlayerApi_0.SetVelocity(Vector3.zero);
                Frozen = true;
            }
            else
            {
                Physics.gravity = _originalGravity;
                if (RestoreVelocity) Networking.LocalPlayer.SetVelocity(_originalVelocity);
                Frozen = false;
            }
        }
    }
}
