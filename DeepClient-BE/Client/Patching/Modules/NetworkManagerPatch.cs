using DeepClient.Client.Misc;
using UnityEngine;
using VRC.SDKBase;

namespace DeepClient.Client.Patching.Modules
{
    internal class NetworkManagerPatch
    {
        public static void Patch()
        {
            EasyPatching.EasyPatchMethodPost(typeof(NetworkManager), "OnPlayerJoined", typeof(NetworkManagerPatch), "OnJoinEvent");
            EasyPatching.EasyPatchMethodPost(typeof(NetworkManager), "OnPlayerLeft", typeof(NetworkManagerPatch), "OnJoinEvent");
        }
        internal static void OnJoinEvent(VRCPlayerApi __0)
        {
            if (ConfManager.playerLogger.Value)
            {
                DeepConsole.Log("PLogger", $"{__0.displayName} has joined.");
            }
            if (ConfManager.VRCAdminStaffLogger.Value && __0.isModerator)
            {
                string alertMessage = $"There is a VRChat mod in the lobby!\nName: {__0.displayName}";
                for (int i = 0; i < 3; i++)
                {
                    PopupHelper.AlertPopup("ALERT: [MODERATOR/ADMIN]", alertMessage, 20);
                    DeepConsole.Log("ALERT", alertMessage);
                }
            }
            if (ConfManager.AntiQuest.Value && __0.isModerator)
            {
                __0.gameObject.SetActive(false);
                DeepConsole.Log("AntiQuest", $"Locally Blocked Quest Player -> {__0.displayName}");
            }
        }
        internal static void OnLeaveEvent(PlayerNet_Internal __0)
        {
            if (__0.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_VRCPlayerApi_0.isLocal)
            {
                Module.QOL.LastInstanceRejoiner.SaveInstanceID();
                return;
            }
            if (ConfManager.playerLogger.Value)
            {
                DeepConsole.Log("PLogger", $"{__0.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.displayName} has joined.");
            }
        }
    }
}
