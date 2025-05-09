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
            DeepConsole.LogConsole("NetworkManagerPatch", "Initialized");
            
            
            VRCEventDelegate<Player> onPlayerJoinedDelegate = NetworkManager.field_Internal_Static_NetworkManager_0.OnPlayerJoinedDelegate;
            VRCEventDelegate<Player> onPlayerAwakeDelegate = NetworkManager.field_Internal_Static_NetworkManager_0.OnPlayerAwakeDelegate;
            VRCEventDelegate<Player> onPlayerLeaveDelegate = NetworkManager.field_Internal_Static_NetworkManager_0.OnPlayerLeaveDelegate;
            System.Action<Player> action1 = (Player p) =>
            {
                if (p != null)
                {
                    OnJoinEvent(p);
                }
                else
                {
                    DeepConsole.LogConsole("NetworkManagerPatch", "OnPlayerJoined received a null Player object!");
                }
            };
            
            try
            {
                UnityAction<Player> playerJoinedAction = action1;
                onPlayerJoinedDelegate.field_Private_HashSet_1_UnityAction_1_T_0.Add(playerJoinedAction);
            }
            catch (Exception e)
            {
                DeepConsole.LogConsole("NetworkManagerPatch", "Failed to add OnPlayerJoined delegate handler!");
                DeepConsole.LogException(e);
            }
            
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
            
            
            //Custom nameplate
            try
            {
                // Add the CustomNameplate component to the joining player's GameObject
                var nameplate = __0.gameObject.AddComponent<CustomNameplate>(); 
                // Assign the Player object to the component so it knows which player to track
                nameplate.Player = __0; 
                DeepConsole.LogConsole("NetworkManagerPatch",$"Added CustomNameplate to {__0.field_Private_APIUser_0.displayName}");
            }
            catch (Exception e)
            {
                DeepConsole.LogConsole("NetworkManagerPatch","ConfManager.customnameplate.Value HA REVENTADO");
                DeepConsole.LogException(e);
            }
            
            //Custom nameplate Account Age
            try
            {
                // Add the component first
                CustomNameplateAccountAge nameplateAge = __0.gameObject.AddComponent<CustomNameplateAccountAge>();
                nameplateAge.Player = __0; // Assign player reference if needed by the component

                // Define success callback for FetchUser
                Action<APIUser> successCallback = (APIUser fetchedUser) =>
                {
                    if (nameplateAge != null && fetchedUser != null) // Check if component still exists and user data is valid
                    {
                        // Assuming CustomNameplateAccountAge has a field like 'playerAge' or 'joinDate'
                        // Adjust the field name ('playerAge') as necessary!
                        nameplateAge.playerAge = fetchedUser.date_joined;
                         DeepConsole.LogConsole("NetworkManagerPatch",$"Fetched join date ({fetchedUser.date_joined}) for {fetchedUser.displayName}");
                    }
                     else
                    {
                         DeepConsole.LogConsole("NetworkManagerPatch",$"SuccessCallback: Component or fetchedUser was null for {__0.field_Private_APIUser_0.displayName}.");
                    }
                };

                // Define error callback for FetchUser
                Action<string> errorCallback = (string errorMessage) =>
                {
                    DeepConsole.LogConsole("NetworkManagerPatch", $"Failed to fetch user data for {__0.field_Private_APIUser_0.displayName}. Error: {errorMessage}");
                };

                // Fetch the user data asynchronously
                // Ensure __0.field_Private_APIUser_0.id is the correct user ID string
                APIUser.FetchUser(__0.field_Private_APIUser_0.id, successCallback, errorCallback);
                 DeepConsole.LogConsole("NetworkManagerPatch",$"Added CustomNameplateAccountAge and initiated fetch for {__0.field_Private_APIUser_0.displayName}");

            }
            catch (Exception e)
            {
                 DeepConsole.LogConsole("NetworkManagerPatch",$"Failed to add CustomNameplateAccountAge or fetch data for {__0.field_Private_APIUser_0.displayName}.");
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
