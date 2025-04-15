using ImGuiNET;

namespace DeepClient.Client.GUI.Menus
{
    internal class MovementMenu
    {
        public static void Render()
        {
            ImGui.Spacing();
            if (ImGui.Button("Flight"))
            {
                Module.Movement.Flight.FlyToggle();
            }
            ImGui.Spacing();
            if (ImGui.Checkbox("Force Jump", ref Module.Movement.ForceJump.IsEnabled))
            {
                Module.Movement.ForceJump.State(Module.Movement.ForceJump.IsEnabled);
            }
            ImGui.Spacing();
            ImGui.Checkbox("JetPack", ref Module.Movement.Jetpack.Jetpackbool);
            ImGui.Spacing();
            if (ImGui.Checkbox("Pos Saver", ref Module.Movement.PosSaver.IsEnabled))
            {
                Module.Movement.PosSaver.State(Module.Movement.PosSaver.IsEnabled);
            }
            ImGui.Spacing();
            ImGui.Checkbox("RayCastTP", ref Module.Movement.RayCastTP.Enabled);
            ImGui.Spacing();
            ImGui.Checkbox("SpinBot", ref Module.Movement.SpinBot.SpinBotbool);
            ImGui.Spacing();
            if (Module.Movement.SpinBot.SpinBotbool)
            {
                ImGui.SliderFloat("SpinBot Speed", ref Module.Movement.SpinBot.rotationSpeed, 220f, 520f);
            }
        }
    }
}
