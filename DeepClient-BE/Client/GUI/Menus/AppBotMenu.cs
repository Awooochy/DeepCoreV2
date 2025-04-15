using ImGuiNET;
using DeepClient.Client.Module.ApplicationBot;
using VRC.Core;

namespace DeepClient.Client.GUI.Menus
{
    internal class AppBotMenu
    {
        public static bool ServerStarted = false;
        public static bool WithGUi = false;
        public static void Render()
        {
            if (ImGui.CollapsingHeader("Bot Controls"))
            {
                ImGui.Spacing();
                if (ImGui.Button("Start bot 1"))
                {
                    bool flag = !ServerStarted;
                    if (flag)
                    {
                        SocketConnection.StartServer();
                        ServerStarted = true;
                    }
                    Bot.MakeBot(8, WithGUi, false);
                }
                ImGui.SameLine();
                ImGui.Checkbox("With GUI ?", ref WithGUi);
            }
            ImGui.Spacing();
            if (ImGui.CollapsingHeader("Other Controls"))
            {
                ImGui.Spacing();
                if (ImGui.Button("Join To Me [All]"))
                {
                    var CUL = APIUser.CurrentUser.location;
                    SocketConnection.SendCommandToClients($"JoinWorld {CUL}");
                }
                ImGui.SameLine();
                if (ImGui.Button("Join WorldID [All]"))
                {

                }
                ImGui.SameLine();
                if (ImGui.Button("Set Avatar [All]"))
                {

                }
                ImGui.SameLine();
                if (ImGui.Button("Clone My Avatar [All]"))
                {

                }
            }
        }
    }
}
