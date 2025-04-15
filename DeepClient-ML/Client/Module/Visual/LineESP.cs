using System.Collections.Generic;
using DeepCore.Client.Misc;
using UnityEngine;
using VRC;

namespace DeepCore.Client.Module.Visual
{
    internal class LineESP
    {
        public static List<LineRenderer> lineRenderers = new List<LineRenderer>();
        public static List<Transform> otherPlayers = new List<Transform>();
        public static Transform localPlayer;
        public static bool LinepMyESP = false;
        public static void LineState(bool s)
        {
            if (s)
            {
                if (localPlayer == null)
                {
                    localPlayer = Player.prop_Player_0.transform;
                }
                FindOtherPlayers();
            }
            else
            {
                ClearLines();
            }
        }
        public static void FindOtherPlayers()
        {
            otherPlayers.Clear();
            foreach (var player in VrcExtensions.GetAllPlayers())
            {
                if (player.gameObject.transform != localPlayer)
                {
                    otherPlayers.Add(player.gameObject.transform);
                    CreateLineRenderer(player.gameObject.transform, player.field_Private_APIUser_0.GetPlayerColor());
                    LinepMyESP = true;
                }
            }
        }
        public static void CreateLineRenderer(Transform target, Color color)
        {
            GameObject lineObj = new GameObject("PlayerLine");
            LineRenderer lineRenderer = lineObj.AddComponent<LineRenderer>();
            lineRenderer.startWidth = 0.01f;
            lineRenderer.endWidth = 0.01f;
            lineRenderer.material = new Material(Shader.Find("GUI/Text Shader"));
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            lineRenderer.positionCount = 2;
            lineRenderers.Add(lineRenderer);
        }
        public static void UpdateLines()
        {
            if (LinepMyESP)
            {
                if (localPlayer == null || otherPlayers.Count == 0) return;
                for (int i = 0; i < otherPlayers.Count; i++)
                {
                    if (otherPlayers[i] == null || lineRenderers[i] == null) continue;
                    lineRenderers[i].SetPosition(0, localPlayer.position);
                    lineRenderers[i].SetPosition(1, otherPlayers[i].position);
                }
            }
        }
        public static void ClearLines()
        {
            foreach (var lr in lineRenderers)
            {
                if (lr != null)
                    GameObject.Destroy(lr.gameObject);
            }
            lineRenderers.Clear();
            LinepMyESP = false;
        }
    }
}
