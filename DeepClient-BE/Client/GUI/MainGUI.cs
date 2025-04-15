using DeepClient.Client.GUI.Menus;
using ImGuiNET;

namespace DeepClient.Client.GUI
{
    internal class MainGUI
    {
        public static void RenderMenu()
        {
            GUIStyle.RenderMenu();
            if (DearImGuiInjection.DearImGuiInjection.IsCursorVisible)
            {
                #region yay
                var io = ImGui.GetIO();
                var foregroundDrawList = ImGui.GetForegroundDrawList();
                float mouseSize = 15f;
                ImGui.SetNextWindowSize(new System.Numerics.Vector2(600, 400), ImGuiCond.FirstUseEver);
                #endregion
                ImGui.Begin("DeepClient");
                if (ImGui.BeginTabBar("TabsBar"))
                {
                    if (ImGui.BeginTabItem("Visuals"))
                    {
                        VisualMenu.Render();
                        ImGui.EndTabItem();
                    }
                    if (ImGui.BeginTabItem("Mouvements"))
                    {
                        MovementMenu.Render();
                        ImGui.EndTabItem();
                    }
                    if (ImGui.BeginTabItem("World Hacks"))
                    {
                        WorldHackMenu.Render();
                        ImGui.EndTabItem();
                    }
                    if (ImGui.BeginTabItem("Exploits"))
                    {
                        ExploitMenu.Render();
                        ImGui.EndTabItem();
                    }
                    if (ImGui.BeginTabItem("Player Selector"))
                    {
                        PSelectorMenu.Render();
                        ImGui.EndTabItem();
                    }
                    if (ImGui.BeginTabItem("Settings"))
                    {
                        SettingsMenu.Render();
                        ImGui.EndTabItem();
                    }
                    ImGui.EndTabBar();
                }
                ImGui.End();
            }
        }
    }
}