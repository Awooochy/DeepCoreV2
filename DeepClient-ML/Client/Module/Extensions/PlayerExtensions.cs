using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC;
using VRC.Core;
using VRC.SDKBase;

namespace DeepCore.Client.Module.ApplicationBot
{
    internal static class PlayerExtensions
    {
        public static VRCPlayer LocalVRCPlayer
        {
            get
            {
                return VRCPlayer.field_Internal_Static_VRCPlayer_0;
            }
        }
        public static Player LocalPlayer
        {
            get
            {
                return Player.prop_Player_0;
            }
        }
        public static PlayerManager PManager
        {
            get
            {
                return PlayerManager.prop_PlayerManager_0;
            }
        }
        public static List<Player> AllPlayers
        {
            get
            {
                return PManager.field_Private_List_1_Player_0.ToArray().ToList<Player>();
            }
        }
        public static VRCPlayerApi GetVRCPlayerApi(this Player player)
        {
            return player.field_Private_VRCPlayerApi_0;
        }
        public static APIUser GetAPIUser(this Player player)
        {
            return player.field_Private_APIUser_0;
        }
        public static float LocalGain
        {
            get
            {
                return USpeaker.field_Internal_Static_Single_1;
            }
            set
            {
                USpeaker.field_Internal_Static_Single_1 = value;
            }
        }
        public static void SendVRCEvent(VRC_EventHandler.VrcEvent vrcEvent, VRC_EventHandler.VrcBroadcastType type, GameObject instagator)
        {
            bool flag = PlayerExtensions.handler == null;
            if (flag)
            {
                handler = Resources.FindObjectsOfTypeAll<VRC_EventHandler>().FirstOrDefault<VRC_EventHandler>();
            }
            vrcEvent.ParameterObject = handler.gameObject;
            handler.TriggerEvent(vrcEvent, type, instagator, 0f);
        }
        public static GameObject InstantiatePrefab(string PrefabNAME, Vector3 position, Quaternion rotation)
        {
            return Networking.Instantiate(0, PrefabNAME, position, rotation);
        }
        public static Player GetPlayerByUserID(string UserID)
        {
            return (from p in AllPlayers
                    where p.GetAPIUser().id == UserID
                    select p).FirstOrDefault<Player>();
        }
        public static void SetGain(float Gain)
        {
            LocalGain = Gain;
        }
        private static VRC_EventHandler handler;
    }
}
