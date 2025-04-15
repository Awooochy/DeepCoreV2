using DeepCore.Client.Misc;
using UnityEngine;
using VRC.SDKBase;

namespace DeepCore.Client.Module.Movement
{
    internal class Jetpack
    {
        public static bool Jetpackbool;
        public static void Update()
        {
            if (Jetpackbool && (Binds.Button_Jump.GetState(0) || Input.GetKey((KeyCode)32)))
            {
                Vector3 velocity = Networking.LocalPlayer.GetVelocity();
                velocity.y = Networking.LocalPlayer.GetJumpImpulse();
                Networking.LocalPlayer.SetVelocity(velocity);
            }
        }
    }
}
