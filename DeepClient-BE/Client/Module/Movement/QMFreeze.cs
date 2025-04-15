using UnityEngine;
using VRC.SDKBase;

namespace DeepClient.Client.Module.Movement
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
            if (ConfManager.ShouldQMFreeze.Value)
            {
                if (s)
                {
                    _originalGravity = Physics.gravity;
                    _originalVelocity = VRCPlayer.field_Internal_Static_MonoBehaviour1PublicOb_pOb_s_pBoGaOb_pStUnique_0.field_Private_VRCPlayerApi_0.GetVelocity();
                    if (_originalVelocity == Vector3.zero) return;
                    Physics.gravity = Vector3.zero;
                    VRCPlayer.field_Internal_Static_MonoBehaviour1PublicOb_pOb_s_pBoGaOb_pStUnique_0.field_Private_VRCPlayerApi_0.SetVelocity(Vector3.zero);
                    Frozen = true;
                }
                else
                {
                    Physics.gravity = _originalGravity;
                    if (RestoreVelocity) VRCPlayer.field_Internal_Static_MonoBehaviour1PublicOb_pOb_s_pBoGaOb_pStUnique_0.field_Private_VRCPlayerApi_0.SetVelocity(_originalVelocity);
                    Frozen = false;
                }
            }
        }
    }
}
