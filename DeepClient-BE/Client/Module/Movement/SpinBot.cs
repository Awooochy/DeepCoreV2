using UnityEngine;
using VRC.SDKBase;

namespace DeepClient.Client.Module.Movement
{
    internal class SpinBot
    {
        public static float rotationSpeed = 220f;
        public static bool SpinBotbool;
        public static void Update()
        {
            if (SpinBotbool)
            {
                Networking.LocalPlayer.gameObject.transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }
    }
}