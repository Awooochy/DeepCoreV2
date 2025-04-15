using UnityEngine;
using VRC.SDKBase;

namespace DeepClient.Client.Module.Movement
{
    internal class RayCastTP
    {
        public static bool Enabled = false;
        public static void Update()
        {
            if (Enabled)
            {
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                    if (Physics.Raycast(ray, out RaycastHit raycastHit)) Networking.LocalPlayer.gameObject.transform.position = raycastHit.point;
                }
            }
        }
    }
}