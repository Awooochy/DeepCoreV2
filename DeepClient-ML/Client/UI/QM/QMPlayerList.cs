using System.Collections;
using System.Text;
using DeepCore.Client.Misc;
using TMPro;
using UnityEngine;
using UnityEngine.XR;
using VRC.SDKBase;

namespace DeepCore.Client.UI.QM
{
    internal class QMPlayerList
    {
        public static bool IsLoaded = false;
        public static StringBuilder sb = new StringBuilder();
        public static IEnumerator StartPlayerList()
        {
            while (GameObject.Find("Canvas_QuickMenu(Clone)") == null)
            {
                yield return null;
            }
            new GameObject("DPlayerList").transform.parent = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon").transform;
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").transform.position = new Vector3(-8.2154f, 0.9213f, 3.0299f);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").transform.localPosition = new Vector3(373.3873f, -186.1004f, -353.249f);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").transform.localScale = new Vector3(7f, 7f, 7f);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").AddComponent<TextMeshProUGUI>();
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").GetComponent<TextMeshProUGUI>().fontSize = 3f;
            if (XRSettings.isDeviceActive)
            {
                GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").transform.position = new Vector3(-0.1798f, 1.4831f, -0.3481f);
                GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").transform.localPosition = new Vector3(341.9142f, -14.9804f, -14.9804f);
            }
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").GetComponent<TextMeshProUGUI>().text = "Playerlist is having error !!!";
            IsLoaded = true;
            yield break;
        }
        public static void UpdateList()
        {
            if (IsLoaded)
            {
                if (Networking.LocalPlayer == null)
                    return;
                sb.Clear();
                sb.AppendLine($"<size=120%>({VRCPlayerApi.AllPlayers.Count}) players in room</size>\n");
                foreach (var player in VRCPlayerApi.AllPlayers)
                {
                    PlayerNet playerNet = player.gameObject.GetComponent<PlayerNet>();
                    if (playerNet == null)
                        continue;
                    int fps = playerNet.field_Private_Byte_0;
                    int ping = playerNet.field_Private_Int16_0;
                    Color fpsColor = Color.Lerp(Color.red, Color.green, Mathf.Clamp01((float)fps / 100f));
                    string fpsText = $"<color=#{ColorUtility.ToHtmlStringRGB(fpsColor)}>[{fps} FPS]</color>";
                    Color pingColor = Color.Lerp(Color.green, Color.red, Mathf.Clamp01((float)ping / 150f));
                    string pingText = $"<color=#{ColorUtility.ToHtmlStringRGB(pingColor)}>[{ping} ms]</color>";
                    string masterText = player.isMaster ? "<color=red>[M]</color> - " : "";
                    string friendText = playerNet.prop_VRCPlayer_0._player.field_Private_APIUser_0.isFriend ? "<color=yellow>{F]</color> - " : "";
                    Color playerColor = playerNet.prop_VRCPlayer_0._player.field_Private_APIUser_0.GetPlayerColor();
                    string nameText = $"<color=#{ColorUtility.ToHtmlStringRGB(playerColor)}>{player.displayName}</color>";
                    sb.AppendLine($"{nameText} - {masterText}{friendText}{fpsText} - {pingText}");
                }
                GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Left/Button/Icon/DPlayerList").GetComponent<TextMeshProUGUI>().text = sb.ToString();
            }
        }
    }
}
