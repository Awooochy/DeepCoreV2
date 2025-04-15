using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepClient.Client.Misc;
using TMPro;
using UnityEngine;
using VRC.SDKBase;

namespace DeepClient.Client.Mono
{
    internal class QMPlayerListMono
    {
        public TextMeshProUGUI textMeshPro;
        private StringBuilder sb = new StringBuilder();

        private void Update()
        {
            if (Networking.LocalPlayer == null || textMeshPro == null)
                return;

            sb.Clear();
            sb.AppendLine($"<size=120%>{VRCPlayerApi.AllPlayers.Count} players online</size>\n");

            foreach (var player in VRCPlayerApi.AllPlayers)
            {
                PlayerNet_Internal playerNet = player.GetPlayer()._playerNet;
                int fps = playerNet.GetFramerate();
                int ping = playerNet.GetPing();

                // FPS Color (Red to Green)
                Color fpsColor = Color.Lerp(Color.red, Color.green, Mathf.Clamp01((float)fps / 100f));
                string fpsText = $"<color=#{ColorUtility.ToHtmlStringRGB(fpsColor)}>[{fps} FPS]</color>";

                // Ping Color (Green to Red)
                Color pingColor = Color.Lerp(Color.green, Color.red, Mathf.Clamp01((float)ping / 150f));
                string pingText = $"<color=#{ColorUtility.ToHtmlStringRGB(pingColor)}>[{ping} ms]</color>";

                // Master Tag
                string masterText = player.isMaster ? "<color=yellow>Master</color> - " : "";

                // Player Name Color
                Color playerColor = player.GetPlayer().prop_APIUser_0.GetPlayerColor();
                string nameText = $"<color=#{ColorUtility.ToHtmlStringRGB(playerColor)}>{player.displayName}</color>";

                // Combine player info
                sb.AppendLine($"{masterText}{fpsText} - {pingText} - {nameText}");
            }

            textMeshPro.text = sb.ToString();
        }
    }
}
