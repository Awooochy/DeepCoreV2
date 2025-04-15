using UnityEngine;
using VRC.SDKBase;

namespace DeepCore.Client.Module.Movement
{
    internal class RayCastTP
    {
        public static bool Enabled = false;
        public static bool Pickup2Click = false;
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
            if (Pickup2Click)
            {
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
                    if (Physics.Raycast(ray, out RaycastHit raycastHit))
                        foreach (VRC_Pickup vrc_Pickup in GameObject.FindObjectsOfType<VRC_Pickup>())
                        {
                            Networking.SetOwner(Networking.LocalPlayer, vrc_Pickup.gameObject);
                            vrc_Pickup.transform.position = raycastHit.point;
                        }
                }
            }
        }
    }
}
