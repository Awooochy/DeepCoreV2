using ImGuiNET;
using VRC.SDKBase;

namespace DeepClient.Client.GUI.Menus
{
    internal class PSelectorMenu
    {
        public static void Render()
        {
            if (ImGui.BeginMenu("TPObj to player"))
            {
                foreach (var player in VRCPlayerApi.AllPlayers)
                {
                    if (ImGui.MenuItem($">: " +player.displayName)) { Module.WorldHacks.PickupUtils.TPP2P(player); }
                }
                ImGui.EndMenu();
            }
            if (ImGui.BeginMenu("Listen Player"))
            {
                foreach (var player in VRCPlayerApi.AllPlayers)
                {
                    if (ImGui.MenuItem($">: " +player.displayName)) { Misc.VrcExtensions.ListenPlayer(player); }
                }
                ImGui.EndMenu();
            }
            if (ImGui.BeginMenu("Force Lewd"))
            {
                foreach (var player in VRCPlayerApi.AllPlayers)
                {
                    if (ImGui.MenuItem($">: " +player.displayName)) { Module.Exploits.ForceLewd.LewdPlayer(player); }
                }
                ImGui.EndMenu();
            }
        }
    }
}
