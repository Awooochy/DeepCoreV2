using ImGuiNET;
using VRC.SDKBase;

namespace DeepClient.Client.GUI.Menus
{
    internal class WorldHackMenu
    {
        public static void Render()
        {
            if (ImGui.BeginMenu("Murder 4"))
            {
                if (ImGui.MenuItem("Start match.")) {Module.WorldHacks.Murder4.StartMatch(); }
                if (ImGui.MenuItem("Stop match.")) {Module.WorldHacks.Murder4.StopMatch(); }
                if (ImGui.MenuItem("Become halal.")) {Module.WorldHacks.Murder4.BeARole(Networking.LocalPlayer.displayName, "SyncAssignB"); }
                if (ImGui.MenuItem("Become habibi.")) {Module.WorldHacks.Murder4.BeARole(Networking.LocalPlayer.displayName, "SyncAssignM"); }
                if (ImGui.MenuItem("Reshow everyone roles.")) {Module.WorldHacks.Murder4.ReshowEveryoneRoles(); }
                if (ImGui.MenuItem("Halal win.")) {Module.WorldHacks.Murder4.BystandersWin(); }
                if (ImGui.MenuItem("Habibi win.")) {Module.WorldHacks.Murder4.MurderWin(); }
                if (ImGui.MenuItem("Bring Revolver.")) {Module.WorldHacks.Murder4.BringRevolver(); }
                if (ImGui.MenuItem("Bring Shotgun.")) {Module.WorldHacks.Murder4.BringShotgun(); }
                if (ImGui.MenuItem("Bring Luger.")) {Module.WorldHacks.Murder4.BringLuger(); }
                if (ImGui.MenuItem("Fire Shotgun.")) {Module.WorldHacks.Murder4.fireShotgun(); }
                if (ImGui.MenuItem("Fire Revolver.")) {Module.WorldHacks.Murder4.firerevolver(); }
                if (ImGui.MenuItem("Fire Luger.")) {Module.WorldHacks.Murder4.fireLuger(); }
                if (ImGui.MenuItem("Revolver Patron Skin.")) {Module.WorldHacks.Murder4.RevolverPatronSkin(); }
                if (ImGui.MenuItem("AllaFraguakbar.")) {Module.WorldHacks.Murder4.Frag0Explode(); }
                if (ImGui.MenuItem("Release da Snake.")) {Module.WorldHacks.Murder4.ReleaseSnake(); }
                if (ImGui.MenuItem("ERP.","Haram.")) {Module.WorldHacks.Murder4.CloseAllDoors(); }
                if (ImGui.MenuItem("UNERP.","Not Haram.")) {Module.WorldHacks.Murder4.UnlockAllDoors(); }
                ImGui.EndMenu();
            }
            if (ImGui.BeginMenu("Among Us"))
            {
                ImGui.EndMenu();
            }
        }
    }
}
