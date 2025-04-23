using System;
using System.Linq;
using DeepCore.Client.Module.ApplicationBot;
using Il2CppSystem.Collections.Generic;
using ReMod.Core.VRChat;
using UnityEngine;
using UnityEngine.XR;
using VRC;
using VRC.Core;
using VRC.SDKBase;

namespace DeepCore.Client.Misc
{
    public static class PlayerUtil
    {
        public static bool IsInVR
        {
            get
            {
                bool cachedVRState = _cachedVRState;
                bool isInVrCache;
                if (cachedVRState)
                {
                    isInVrCache = _isInVrCache;
                }
                else
                {
                    _cachedVRState = true;
                    List<XRDisplaySubsystem> list = new List<XRDisplaySubsystem>();
                    SubsystemManager.GetInstances<XRDisplaySubsystem>(list);
                    foreach (XRDisplaySubsystem xrdisplaySubsystem in list)
                    {
                        bool flag = !xrdisplaySubsystem.running;
                        if (!flag)
                        {
                            _isInVrCache = true;
                            break;
                        }
                    }
                    isInVrCache = _isInVrCache;
                }
                return isInVrCache;
            }
        }
        public static ApiAvatar GetAvatarInfo(this Player player)
        {
            return (player != null) ? player.prop_ApiAvatar_0 : null;
        }
        public static bool IsInRoom
        {
            get
            {
                bool flag = RoomManager.field_Internal_Static_ApiWorld_0 != null;
                return flag && RoomManager.field_Private_Static_ApiWorldInstance_0 != null;
            }
        }
        public static APIUser fetchbyid(string id)
        {
            APIUser usr = null;
            APIUser.FetchUser(id, new Action<APIUser>((a) => { usr = a; }), new Action<string>((a) => { DeepConsole.Log("IDFetcher", a); }));
            return usr;
        }
        public static int GetPlayerRank(VRCPlayer player)
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            foreach (string text in player._player.field_Private_APIUser_0.tags)
            {
                if (text.Contains("basic"))
                {
                    flag = true;
                }
                else if (text.Contains("known"))
                {
                    flag2 = true;
                }
                else if (text.Contains("trusted"))
                {
                    flag3 = true;
                }
                else if (text.Contains("veteran"))
                {
                    flag4 = true;
                }
                else if (text.Contains("troll"))
                {
                    flag5 = true;
                }
            }
            if (player._player.field_Private_APIUser_0.isFriend)
            {
                return 6;
            }
            if (flag5)
            {
                return 5;
            }
            if (flag4)
            {
                return 4;
            }
            if (flag3)
            {
                return 3;
            }
            if (flag2)
            {
                return 2;
            }
            if (flag)
            {
                return 1;
            }
            return -1;
        }
        internal static bool AnyActionMenuesOpen()
        {
            return ActionMenuController.field_Public_Static_ActionMenuController_0.field_Public_ActionMenuOpener_0.field_Private_Boolean_0 || ActionMenuController.field_Public_Static_ActionMenuController_0.field_Public_ActionMenuOpener_1.field_Private_Boolean_0;
        }
        public static Player LocalPlayer()
        {
            return Player.prop_Player_0;
        }
        public static string ColourFPS(Player player)
        {
            float num = (player._playerNet.field_Private_Byte_0 != 0) ? Mathf.Floor(1000f / (float)player._playerNet.field_Private_Byte_0) : -1f;
            string str;
            if (num > 55f)
            {
                str = "<color=green>";
            }
            else if (num <= 55f && num > 18f)
            {
                str = "<color=yellow>";
            }
            else if (num <= 18f && num > 5f)
            {
                str = "<color=orange>";
            }
            else
            {
                str = "<color=red>";
            }
            return str + num.ToString() + "</color>";
        }
        public static string GetAvatarStatus(this Player player)
        {
            string text = player.GetAvatarInfo().releaseStatus.ToLower();
            bool flag = text == "public";
            string result;
            if (flag)
            {
                result = "<color=green>" + text + "</color>";
            }
            else
            {
                result = "<color=red>" + text + "</color>";
            }
            return result;
        }
        internal static bool IsAdmin(this Player player)
        {
            return player.field_Private_APIUser_0.hasModerationPowers || player.field_Private_APIUser_0.tags.Contains("admin_moderator") || player.field_Private_APIUser_0.hasSuperPowers || player.field_Private_APIUser_0.tags.Contains("admin_");
        }
        public static string GetPlatform(this Player player)
        {
            bool flag = player != null;
            string result;
            if (flag)
            {
                bool isOnMobile = player.field_Private_APIUser_0.IsOnMobile;
                if (isOnMobile)
                {
                    result = "<color=green>Q</color>";
                }
                else
                {
                    bool flag2 = player.GetVrcPlayerApi().IsUserInVR();
                    if (flag2)
                    {
                        result = "<color=#CE00D5>V</color>";
                    }
                    else
                    {
                        result = "<color=grey>PC</color>";
                    }
                }
            }
            else
            {
                result = "";
            }
            return result;
        }
        public static string GetFramesColord(this Player player)
        {
            float frames = player.GetFrames();
            bool flag = frames > 80f;
            string result;
            if (flag)
            {
                result = "<color=green>" + frames.ToString() + "</color>";
            }
            else
            {
                bool flag2 = frames > 30f;
                if (flag2)
                {
                    result = "<color=yellow>" + frames.ToString() + "</color>";
                }
                else
                {
                    result = "<color=red>" + frames.ToString() + "</color>";
                }
            }
            return result;
        }
        internal static string GetAviColord(this Player player)
        {
            string releaseStatus = player.prop_ApiAvatar_0.releaseStatus;
            bool flag = releaseStatus == "internal";
            string result;
            if (flag)
            {
                result = " | [<color=green>Public</color>]";
            }
            else
            {
                result = " | [<color=red>Private</color>]";
            }
            return result;
        }
        public static string IsPlayerMicDisabled(this Player player)
        {
            if (!(player.prop_VRCPlayer_0.field_Internal_Animator_0 == null) && player.prop_VRCPlayer_0.field_Internal_Animator_0.GetBool("MuteSelf"))
            {
                return "<color=red>🔇</color>";
            }
            return "<color=green>🎤</color>";
        }

        public static float GetQuality(this VRCPlayer Instance)
        {
            return Instance.GetPlayerNet().prop_Single_1;
        }
        public static string GetPingColord(this Player player)
        {
            short ping = player.GetPing();
            bool flag = ping > 150;
            string result;
            if (flag)
            {
                result = "<color=red>" + ping.ToString() + "</color>";
            }
            else
            {
                bool flag2 = ping > 75;
                if (flag2)
                {
                    result = "<color=yellow>" + ping.ToString() + "</color>";
                }
                else
                {
                    result = "<color=green>" + ping.ToString() + "</color>";
                }
            }
            return result;
        }
        public static Color Friend()
        {
            return VRCPlayer.field_Internal_Static_Color_7;
        }
        public static Color Trusted()
        {
            return VRCPlayer.field_Internal_Static_Color_6;
        }
        public static Color Known()
        {
            return VRCPlayer.field_Internal_Static_Color_5;
        }
        public static Color User()
        {
            return VRCPlayer.field_Internal_Static_Color_4;
        }
        public static Color NewUser()
        {
            return VRCPlayer.field_Internal_Static_Color_3;
        }
        public static Color Visitor()
        {
            return VRCPlayer.field_Internal_Static_Color_2;
        }
        public static Color Troll()
        {
            return VRCPlayer.field_Internal_Static_Color_0;
        }
        public static float GetFrames(this Player player)
        {
            return (player._playerNet.field_Private_Byte_0 != 0) ? Mathf.Floor(1000f / (float)player._playerNet.field_Private_Byte_0) : -1f;
        }
        public static short GetPing(this Player player)
        {
            return player._playerNet.field_Private_Int16_0;
        }
        public static bool ClientDetect(this Player player)
        {
            return player.GetFrames() > 200f || player.GetFrames() < 1f || player.GetPing() > 665 || player.GetPing() < 0;
        }
        public static bool GetIsMaster(this Player player)
        {
            bool flag = player != null;
            if (flag)
            {
                try
                {
                    return player.GetVrcPlayerApi().isMaster;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }
        public static VRCPlayer GetLocalVRCPlayer()
        {
            return VRCPlayer.field_Internal_Static_VRCPlayer_0;
        }
        public static VRCPlayerApi GetVrcPlayerApi(this Player player)
        {
            return (player != null) ? player.field_Private_VRCPlayerApi_0 : null;
        }
        public static VRCPlayer GetVRCPlayer()
        {
            return VRCPlayer.field_Internal_Static_VRCPlayer_0;
        }
        public static Player[] GetAllPlayers()
        {
            return PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray();
        }
        public static Player GetPlayerNewtworkId(this int id)
        {
            return (from player in GetAllPlayers()
                    where player._vrcplayer.field_Private_VRCPlayerApi_0.playerId == id
                    select player).FirstOrDefault<Player>();
        }
        public static Player GetPlayerNewtworkedId(this int id)
        {
            return (from player in PlayerUtil.GetAllPlayers()
                    where player.prop_PlayerNet_0.field_Private_Int32_0 == id
                    select player).FirstOrDefault<Player>();
        }
        public static APIUser GetUserById(this string userid)
        {
            return (from player in PlayerManager.prop_PlayerManager_0.field_Private_List_1_Player_0.ToArray()
                    where player.field_Private_APIUser_0.id == userid
                    select player).FirstOrDefault<Player>().field_Private_APIUser_0;
        }
        public static bool IsFriend(this Player player)
        {
            return APIUser.CurrentUser.friendIDs.Contains(player.field_Private_APIUser_0.id);
        }
        public static string phontonnum(this Player player)
        {
            return player.prop_Int32_0.ToString();
        }
        public static string Current_World_ID
        {
            get
            {
                return RoomManager.field_Internal_Static_ApiWorld_0.id + ":" + RoomManager.field_Private_Static_ApiWorldInstance_0.instanceId;
            }
        }
        internal static APIUser GetAPIUser(this Player player)
        {
            return player.field_Private_APIUser_0;
        }
        public static Player GetPhotonPlayer(this Player player)
        {
            return player.field_Private_Player_0.field_Public_Player_0;
        }
        public static Player GetPlayerInformationById(int index)
        {
            foreach (Player player in GetAllPlayers())
            {
                bool flag = player._playerNet.field_Private_Int32_0 == index;
                if (flag)
                {
                    return player;
                }
            }
            return null;
        }
        internal static string GetRankColord(this Player player)
        {
            bool flag = player.field_Private_APIUser_0.hasModerationPowers || player.field_Private_APIUser_0.tags.Contains("admin_moderator");
            bool flag2 = player.field_Private_APIUser_0.hasSuperPowers || player.field_Private_APIUser_0.tags.Contains("admin_");
            bool flag3 = flag2;
            string result;
            if (flag3)
            {
                result = "<color=#ff0000>[Admin User]</color>";
            }
            else
            {
                bool flag4 = flag;
                if (flag4)
                {
                    result = "<color=red>[Moderation User]</color>";
                }
                else
                {
                    bool hasVeteranTrustLevel = player.field_Private_APIUser_0.hasVeteranTrustLevel;
                    if (hasVeteranTrustLevel)
                    {
                        result = "<color=#864EDD>Trusted</color>";
                    }
                    else
                    {
                        bool hasTrustedTrustLevel = player.field_Private_APIUser_0.hasTrustedTrustLevel;
                        if (hasTrustedTrustLevel)
                        {
                            result = "<color=yellow>Known</color>";
                        }
                        else
                        {
                            bool hasKnownTrustLevel = player.field_Private_APIUser_0.hasKnownTrustLevel;
                            if (hasKnownTrustLevel)
                            {
                                result = "<color=green>User</color>";
                            }
                            else
                            {
                                bool hasBasicTrustLevel = player.field_Private_APIUser_0.hasBasicTrustLevel;
                                if (hasBasicTrustLevel)
                                {
                                    result = "<color=blue>New</color>";
                                }
                                else
                                {
                                    result = "<color=white>Vistor</color>";
                                }
                            }
                        }
                    }
                }
            }
            return result;
        }
        public static List<Player> AllPlayers(this PlayerManager Instance)
        {
            return Instance.field_Private_List_1_Player_0;
        }
        public static Player[] GetAllPlayers(this PlayerManager instance)
        {
            return instance.prop_PooledArray_1_Player_0.Array;
        }
        public static Player GetPlayer(this PlayerManager Instance, int Index)
        {
            return Instance.AllPlayers().ToArray()[Index];
        }
        public static Player GetPlayerWithPlayerID(this PlayerManager Instance, int playerID)
        {
            foreach (Player player in Instance.AllPlayers().ToArray())
            {
                if (player.GetVRCPlayerApi().playerId == playerID)
                {
                    return player;
                }
            }
            return null;
        }
        public static bool _isInVrCache;
        public static bool _cachedVRState;
        public static Color defaultNameplateColor;
        public static System.Collections.Generic.List<string> knownBlocks = new System.Collections.Generic.List<string>();
        public static System.Collections.Generic.List<string> knownMutes = new System.Collections.Generic.List<string>();
        internal delegate void AlignTrackingToPlayerDelegate();
    }
}
