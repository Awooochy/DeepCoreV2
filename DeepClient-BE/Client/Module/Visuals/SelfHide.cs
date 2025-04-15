using VRC.SDKBase;

namespace DeepClient.Client.Module.Visuals
{
    internal class SelfHide
    {
        public static bool IsHidden = false;
        internal static void SelfHidePlayer(bool s)
        {
            if (Networking.LocalPlayer == null)
            {
                return;
            }
            IsHidden = s;
            var forwardDirection = VRCPlayer.field_Internal_Static_MonoBehaviour1PublicOb_pOb_s_pBoGaOb_pStUnique_0.transform.Find("ForwardDirection");
            if (forwardDirection != null)
            {
                forwardDirection.gameObject.SetActive(!IsHidden);
            }
            else
            {
                DeepConsole.Log("DEBUG", "ForwardDirection not found in the player's transform.");
            }
        }
    }
}