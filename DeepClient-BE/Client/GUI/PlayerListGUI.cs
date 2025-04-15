using DeepClient.Client.Misc;
using ImGuiNET;
using VRC.SDKBase;
using UnityEngine;

namespace DeepClient.Client.GUI
{
    internal class PlayerListGUI
    {
        public static System.Numerics.Vector2 listSize = new System.Numerics.Vector2(300, 500);

        public static void RenderPL()
        {
            if (Networking.LocalPlayer == null)
                return;
            ImGui.SetNextWindowPos(new System.Numerics.Vector2(ImGui.GetIO().DisplaySize.X - listSize.X - 10, 10));
            ImGui.SetNextWindowSize(listSize);
            if (ImGui.Begin("##PlayerList", ImGuiWindowFlags.NoCollapse | ImGuiWindowFlags.NoResize | ImGuiWindowFlags.NoTitleBar | ImGuiWindowFlags.NoBackground))
            {
                ImGui.Text($"Users in room ({VRCPlayerApi.AllPlayers.Count}):");
                ImGui.Spacing();
                foreach (var player in VRCPlayerApi.AllPlayers)
                {
                    PlayerNet_Internal playerNet = player.GetPlayer()._playerNet;
                    int fps = playerNet.GetFramerate();
                    int ping = playerNet.GetPing();

                    float fpsRatio = Mathf.Clamp((float)fps / 100f, 0f, 1f);
                    System.Numerics.Vector4 fpsColor = System.Numerics.Vector4.Lerp(new System.Numerics.Vector4(1, 0, 0, 1), new System.Numerics.Vector4(0, 1, 0, 1), fpsRatio);

                    float pingRatio = Mathf.Clamp((float)ping / 150f, 0f, 1f);
                    System.Numerics.Vector4 pingColor = System.Numerics.Vector4.Lerp(new System.Numerics.Vector4(0, 1, 0, 1), new System.Numerics.Vector4(1, 0, 0, 1), pingRatio);

                    string masterText = player.isMaster ? "[M] - " : "";
                    string displayName = player.displayName;
                    ImGui.Text($"{masterText}{displayName}");
                    ImGui.SameLine();
                    ImGui.TextColored(fpsColor, $"- F:{fps} -");
                    ImGui.SameLine();
                    ImGui.TextColored(pingColor, $"P:{ping}");
                }
                ImGui.End();
            }
        }
    }
}
