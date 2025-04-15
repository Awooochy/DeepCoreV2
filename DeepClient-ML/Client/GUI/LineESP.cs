using System.Collections.Generic;
using DeepCore.Client.Misc;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace DeepCore.Client.GUI
{
    internal class LineESP
    {
        public static List<Transform> otherPlayers = new List<Transform>();
        public static bool GUILinepMyESP = false;
        public static Transform localPlayer;
        private static readonly List<GameObject> lineObjects = new List<GameObject>();
        public static void LineState(bool s)
        {
            if (s)
            {
                if (localPlayer == null)
                {
                    localPlayer = Player.prop_Player_0.transform;
                }
                FindOtherPlayers();
                GUILinepMyESP = true;
            }
            else
            {
                ClearLines();
                GUILinepMyESP = false;
            }
        }
        public static void ClearLines()
        {
            otherPlayers.Clear();
            lineObjects.ForEach(GameObject.Destroy);
            lineObjects.Clear();
        }
        public static void FindOtherPlayers()
        {
            otherPlayers.Clear();
            foreach (var player in VrcExtensions.GetAllPlayers())
            {
                var t = player.gameObject?.transform;
                if (t != null && t != Networking.LocalPlayer?.gameObject?.transform)
                {
                    otherPlayers.Add(t);
                }
            }
        }
        public static void Render()
        {
            if (!GUILinepMyESP || Networking.LocalPlayer == null || Camera.main == null)
                return;
            Vector3 screenLocal = new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);
            foreach (Transform other in otherPlayers)
            {
                if (other == null) continue;
                Vector3 screenTarget = Camera.main.WorldToScreenPoint(other.position);
                screenTarget.y = Screen.height - screenTarget.y;
                DrawLine(screenLocal, screenTarget, Color.green, 2f);
            }
        }
        public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width)
        {
            Matrix4x4 matrix = UnityEngine.GUI.matrix;
            Color savedColor = UnityEngine.GUI.color;
            float angle = Vector3.Angle(pointB - pointA, Vector2.right);
            if (pointA.y > pointB.y) angle = -angle;
            float length = (pointB - pointA).magnitude;
            UnityEngine.GUI.color = color;
            GUIUtility.RotateAroundPivot(angle, pointA);
            UnityEngine.GUI.DrawTexture(new Rect(pointA.x, pointA.y, length, width), Texture2D.whiteTexture);
            UnityEngine.GUI.matrix = matrix;
            UnityEngine.GUI.color = savedColor;
        }
    }
}
