using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Valve.VR.InteractionSystem;
using VRC.Core;
using VRC.SDKBase;

namespace DeepClient.Client.Misc
{
    public static class PlayerUtil
    {
        public static int GetFramerate(this PlayerNet_Internal playerNet)
        {
            if (playerNet == null)
                return 0;
            if (playerNet.field_Private_Byte_0 == 0)
                return 0;
            return (int)Mathf.Floor(1000f / playerNet.field_Private_Byte_0);
        }
        public static ApiAvatar GetAvatarInfo(this PlayerNet_Internal player)
        {
            return (player != null) ? player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.prop_ApiAvatar_0 : null;
        }
        public static string GetPingColord(this PlayerNet_Internal player)
        {
            short ping = player.prop_Int16_0; ;
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
        public static string GetFramesColord(this PlayerNet_Internal player)
        {
            float frames = player.GetFramerate();
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
        public static APIUser fetchbyid(string id)
        {
            APIUser usr = null;
            APIUser.FetchUser(id, new Action<APIUser>((a) => { usr = a; }), new Action<string>((a) => { DeepConsole.Log("IDFetcher", a); }));
            return usr;
        }
        public static int GetPlayerRank(PlayerNet_Internal player)
        {
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            bool flag4 = false;
            bool flag5 = false;
            foreach (string text in player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.tags)
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
            if (player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.isFriend)
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
        public static VRCPlayer LocalPlayer()
        {
            return VRCPlayer.field_Internal_Static_MonoBehaviour1PublicOb_pOb_s_pBoGaOb_pStUnique_0;
        }
        public static string GetAvatarStatus(this PlayerNet_Internal player)
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
        internal static bool IsAdmin(this PlayerNet_Internal player)
        {
            return player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.hasModerationPowers || player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.tags.Contains("admin_moderator") || player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.hasSuperPowers || player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.tags.Contains("admin_");
        }
        public static VRCPlayerApi GetVrcPlayerApi(this PlayerNet_Internal player)
        {
            return (player != null) ? player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_VRCPlayerApi_0 : null;
        }
        public static string GetPlatform(this PlayerNet_Internal player)
        {
            bool flag = player != null;
            string result;
            if (flag)
            {
                bool isOnMobile = player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.IsOnMobile;
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
        public static bool IsFriend(this PlayerNet_Internal player)
        {
            return APIUser.CurrentUser.friendIDs.Contains(player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.id);
        }
        public static string phontonnum(this PlayerNet_Internal player)
        {
            return player.prop_Int32_0.ToString();
        }
        internal static APIUser GetAPIUser(this PlayerNet_Internal player)
        {
            return player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0;
        }
        internal static string GetRankColord(this PlayerNet_Internal player)
        {
            bool flag = player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.hasModerationPowers || player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.tags.Contains("admin_moderator");
            bool flag2 = player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.hasSuperPowers || player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.tags.Contains("admin_");
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
                    bool hasVeteranTrustLevel = player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.hasVeteranTrustLevel;
                    if (hasVeteranTrustLevel)
                    {
                        result = "<color=#864EDD>Trusted</color>";
                    }
                    else
                    {
                        bool hasTrustedTrustLevel = player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.hasTrustedTrustLevel;
                        if (hasTrustedTrustLevel)
                        {
                            result = "<color=yellow>Known</color>";
                        }
                        else
                        {
                            bool hasKnownTrustLevel = player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.hasKnownTrustLevel;
                            if (hasKnownTrustLevel)
                            {
                                result = "<color=green>User</color>";
                            }
                            else
                            {
                                bool hasBasicTrustLevel = player.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0.field_Private_APIUser_0.hasBasicTrustLevel;
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
    }
}
