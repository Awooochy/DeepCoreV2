using System;
using DeepCore.Client.Misc;
using DeepCore.Client.Mono;
using UnityEngine.Events;
using VRC;
using VRC.A.Core;
using VRC.Core;

namespace DeepCore.Client.Patching
{
    internal class NetworkManagerPatch
    {
        public static void Patch()
        {
            VRCEventDelegate<Player> onPlayerJoinedDelegate = NetworkManager.field_Internal_Static_NetworkManager_0.OnPlayerJoinedDelegate;
            VRCEventDelegate<Player> onPlayerAwakeDelegate = NetworkManager.field_Internal_Static_NetworkManager_0.OnPlayerAwakeDelegate;
            VRCEventDelegate<Player> onPlayerLeaveDelegate = NetworkManager.field_Internal_Static_NetworkManager_0.OnPlayerLeaveDelegate;
            System.Action<Player> action1 = (Player p) =>
            {
                if (p != null)
                {
                    OnJoinEvent(p);
                }
            };
            UnityAction<Player> playerJoinedAction = action1;
            onPlayerJoinedDelegate.field_Private_HashSet_1_UnityAction_1_T_0.Add(playerJoinedAction);
            System.Action<Player> action = (Player p) =>
            {
                if (p != null)
                {
                    OnLeaveEvent(p);
                }
            };
            UnityAction<Player> playerLeaveAction = action;
            onPlayerLeaveDelegate.field_Private_HashSet_1_UnityAction_1_T_0.Add(playerLeaveAction);
        }
        internal static void OnJoinEvent(Player __0)
        {
            if (__0.field_Private_VRCPlayerApi_0.isLocal)
            {
                Coroutine.CustomMenuBG.ApplyColors();
                Module.QOL.LastInstanceRejoiner.SaveInstanceID();
                Module.Exploits.OwnerNameSpoofer.QuickSpoof();
                return;
            }
            Module.Visual.ESP.OnPlayerJoin();

            try
            {
                if (ConfManager.playerLogger.Value)
                {
                    DeepConsole.Log("PLogger", $"{__0.field_Private_APIUser_0.displayName} has joined.");
                }
            }
            catch(Exception e)
            {
                DeepConsole.LogConsole("NetworkManagerPatch","ConfManager.playerLogger.Value HA REVENTADO");
                DeepConsole.LogException(e);
            }


            try
            {
                if (ConfManager.customnameplate.Value) // Check if the feature is enabled
                {
                    // Add the CustomNameplate component to the joining player's GameObject
                    var nameplate = __0.gameObject.AddComponent<CustomNameplate>(); 
                    // Assign the Player object to the component so it knows which player to track
                    nameplate.Player = __0; 
                }
            }
            catch (Exception e)
            {
                DeepConsole.LogConsole("NetworkManagerPatch","ConfManager.customnameplate.Value HA REVENTADO");
                DeepConsole.LogException(e);
            }


            try
            {
                API.PlayerTagSystem.CheckPlayer(__0);
            }
            catch (Exception e)
            {
                DeepConsole.LogConsole("NetworkManagerPatch","API.PlayerTagSystem.CheckPlayer HA REVENTADO");
                DeepConsole.LogException(e);
            }

            try
            {
                if (ConfManager.VRCAdminStaffLogger.Value && __0.field_Private_APIUser_0.hasModerationPowers)
                {
                    string alertMessage = $"There is a VRChat STAFF in the lobby!\nName: {__0.field_Private_APIUser_0.displayName}";
                    for (int i = 0; i < 3; i++)
                    {
                        VrcExtensions.AlertPopup("ALERT: [MODERATOR/ADMIN]", alertMessage, 20);
                    }
                }
            }
            catch (Exception e)
            {
                DeepConsole.LogConsole("NetworkManagerPatch","ConfManager.VRCAdminStaffLogger.Value HA REVENTADO");
                DeepConsole.LogException(e);
            }
            
            try
            {
                if (ConfManager.AntiQuest.Value && __0.field_Private_APIUser_0.IsOnMobile)
                {
                    __0.gameObject.SetActive(false);
                    DeepConsole.Log("AntiQuest", $"Locally Blocked Quest Player -> {__0.field_Private_APIUser_0.displayName}");
                }
            }
            catch (Exception e)
            {
                DeepConsole.LogConsole("NetworkManagerPatch","ConfManager.AntiQuest.Value HA REVENTADO");
                DeepConsole.LogException(e);
            }
            
            
            
            
        }
        internal static void OnLeaveEvent(Player __0)
        {
            if (__0.field_Private_VRCPlayerApi_0.isLocal)
            {
                return;
            }
            if (ConfManager.playerLogger.Value)
            {
                DeepConsole.Log("PLogger", $"{__0.field_Private_APIUser_0.displayName} has left.");
            }
        }
    }
}
