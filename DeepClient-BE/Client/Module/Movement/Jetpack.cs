using UnityEngine;
using UnityEngine.XR;
using VRC.SDKBase;

namespace DeepClient.Client.Module.Movement
{
    internal class Jetpack
    {
        public static bool Jetpackbool = false;
        public static void Update()
        {
            InputDevice rightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
            bool aButtonPressed = false;
            rightController.TryGetFeatureValue(CommonUsages.primaryButton, out aButtonPressed);
            if (Jetpackbool && (Input.GetKey(KeyCode.Space) || aButtonPressed))
            {
                Vector3 velocity = Networking.LocalPlayer.GetVelocity();
                velocity.y = Networking.LocalPlayer.GetJumpImpulse();
                Networking.LocalPlayer.SetVelocity(velocity);
            }
        }
    }
}