using UnityEngine;
using VRC.SDKBase;

namespace DeepClient.Client.Module.Visuals
{
    internal class ESP
    {
        public static bool isCapEnabled = false;
        public static bool isObjEnabled = false;
        #region PickupsESP
        public static void ObjectState(bool s)
        {
            if (Networking.LocalPlayer == null)
                return;
            foreach (var pickup in GameObject.FindObjectsOfType<VRC_Pickup>())
            {
                var renderers = pickup.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    InputManager.EnableObjectHighlight(renderer.gameObject, s);
                }
            }
        }
        #endregion
        #region CapsuleESP
        public static void CapsuleState(bool s)
        {
            isCapEnabled = s;
            foreach (var player in VRCPlayerApi.AllPlayers)
            {
                if (player.isLocal)
                    continue;
                Transform transform = player.gameObject.transform.Find("SelectRegion");
                InputManager.EnableObjectHighlight(transform.GetComponent<Renderer>(), s);
            }
        }
        #endregion
    }
}
