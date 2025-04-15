using UnityEngine;
using Valve.VR.InteractionSystem;
using VRC;
using VRC.SDKBase;

namespace DeepClient.Client.Module.WorldHacks
{
    internal class PickupUtils
    {
        public static VRC_Pickup[] array;
        public static float rotationAngle = 0f;
        public static void TakeOwnerShipPickup(VRC_Pickup pickup)
        {
            bool flag = pickup == null;
            if (!flag)
            {
                Networking.SetOwner(Networking.LocalPlayer, pickup.gameObject);
            }
        }
        public static void TPP2P(VRCPlayerApi player)
        {
            foreach (VRC_Pickup vrc_Pickup in Object.FindObjectsOfType<VRC_Pickup>())
            {
                Networking.SetOwner(Networking.LocalPlayer, vrc_Pickup.gameObject);
                vrc_Pickup.transform.position = player.gameObject.transform.position + new Vector3(0f, 0.1f, 0f);
            }
            DeepConsole.Log("Pickup",$"Teleported pickups to {player.displayName}");
        }
        public static void Respawn()
        {
            foreach (VRC_Pickup vrc_Pickup in GameObject.FindObjectsOfType<VRC_Pickup>())
            {
                Networking.LocalPlayer.TakeOwnership(vrc_Pickup.gameObject);
                vrc_Pickup.transform.localPosition = new Vector3(0f, -100000f, 0f);
            }
        }
        public static void BringPickups()
        {
            foreach (VRC_Pickup vrc_Pickup in GameObject.FindObjectsOfType<VRC_Pickup>())
            {
                Networking.SetOwner(Networking.LocalPlayer, vrc_Pickup.gameObject);
                vrc_Pickup.transform.position = Networking.LocalPlayer.gameObject.transform.position;
            }
        }
        public static void rotateobjse()
        {
            rotationAngle += 45f;
            if (rotationAngle >= 360f)
            {
                rotationAngle -= 360f;
            }
            foreach (VRC_Pickup vrc_Pickup in GameObject.FindObjectsOfType<VRC_Pickup>())
            {
                Networking.SetOwner(Networking.LocalPlayer, vrc_Pickup.gameObject);
                vrc_Pickup.transform.rotation = Quaternion.Euler(0f, rotationAngle, 0f);
            }
        }
    }
}
