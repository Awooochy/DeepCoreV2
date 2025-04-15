using System;
using System.Collections.Generic;
using DeepCore.Client.Misc;
using UnityEngine;
using VRC;
using VRC.SDKBase;
using VRC.Udon;

namespace DeepCore.Client.Module.Visual
{
    internal class ESP
    {
        public static bool CapMyESP = false;
        #region OnPlayerJoin
        public static void OnPlayerJoin()
        {
            if (CapMyESP)
            {
                foreach (var player in VRCPlayerApi.AllPlayers)
                {
                    if (player.isLocal)
                        continue;
                    Transform transform = player.gameObject.transform.Find("SelectRegion");
                    InputManager.EnableObjectHighlight(transform.GetComponent<Renderer>(), true);
                }
            }
        }
        #endregion
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
            CapMyESP = s;
            foreach (var player in VRCPlayerApi.AllPlayers)
            {
                if (player.isLocal)
                    continue;
                Transform transform = player.gameObject.transform.Find("SelectRegion");
                InputManager.EnableObjectHighlight(transform.GetComponent<Renderer>(), s);
            }
        }
        #endregion
        #region UdonESP
        public static void UdonState(bool s)
        {
            if (Networking.LocalPlayer == null)
                return;
            foreach (var pickup in GameObject.FindObjectsOfType<UdonBehaviour>())
            {
                var renderers = pickup.GetComponentsInChildren<Renderer>();
                foreach (var renderer in renderers)
                {
                    InputManager.EnableObjectHighlight(renderer.gameObject, s);
                }
            }
        }
        #endregion
        #region BetterESP
        public static void CapsuleESP(VRCPlayer player, bool Stat)
        {
            try
            {
                Transform transform = player.transform.Find("SelectRegion");
                HighlightsFXStandalone highlightsFXStandalone = HighlightsFX.field_Private_Static_HighlightsFX_0.gameObject.AddComponent<HighlightsFXStandalone>();
                Color color = VRCPlayer.field_Internal_Static_Color_2;
                int playerRank = PlayerUtil.GetPlayerRank(player);
                if (playerRank != -1)
                {
                    switch (playerRank)
                    {
                        case 1:
                            color = new Color(0.565f, 0.933f, 0.565f);
                            break;
                        case 2:
                            color = new Color(0.565f, 0.933f, 0.565f);
                            break;
                        case 3:
                            color = new Color(1f, 0.792f, 0.365f);
                            break;
                        case 4:
                            color = new Color(0.831f, 0.447f, 1f);
                            break;
                        case 5:
                            color = new Color(1f, 0.459f, 0.459f);
                            break;
                        case 6:
                            color = new Color(1f, 0.984f, 0f);
                            break;
                        default:
                            color = new Color(0.96f, 0.96f, 0.96f);
                            break;
                    }
                    highlightsFXStandalone.blurDownsampleFactor = 1;
                    highlightsFXStandalone.blurIterations = 2;
                }
            }
            catch (Exception ex)
            {
                DeepConsole.LogConsole("Module : ESP",ex.Message);
            }
        }
        #endregion
    }
}
