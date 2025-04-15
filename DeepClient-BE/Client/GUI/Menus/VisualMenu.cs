using ImGuiNET;

namespace DeepClient.Client.GUI.Menus
{
    internal class VisualMenu
    {
        public static void Render()
        {
            ImGui.Spacing();
            if (ImGui.Checkbox("Object ESP", ref Module.Visuals.ESP.isObjEnabled))
            {
                Module.Visuals.ESP.ObjectState(Module.Visuals.ESP.isObjEnabled);
            }
            ImGui.Spacing();
            if (ImGui.Checkbox("Capsule ESP", ref Module.Visuals.ESP.isCapEnabled))
            {
                Module.Visuals.ESP.CapsuleState(Module.Visuals.ESP.isCapEnabled);
            }
            ImGui.Spacing();
            ImGui.Checkbox("OptifineZoom", ref Module.Visuals.OptifineZoom.IsEnabled);
            ImGui.Spacing();
            if (Module.Visuals.OptifineZoom.IsEnabled)
            {
                ImGui.Text("OptifineZoom Settings");
                ImGui.SliderFloat("UnZoomFOV", ref Module.Visuals.OptifineZoom.UnZoomFOV, 60f, 120f);
                ImGui.SliderFloat("ZoomFOV", ref Module.Visuals.OptifineZoom.ZoomFOV, 10f, 120f);
            }
            ImGui.Spacing();
            ImGui.Checkbox("FlipScreen", ref Module.Visuals.FlipScreen.IsEnabled);
            ImGui.Spacing();
            if (ImGui.Checkbox("SelfHide", ref Module.Visuals.SelfHide.IsHidden))
            {
                Module.Visuals.SelfHide.SelfHidePlayer(Module.Visuals.SelfHide.IsHidden);
            }
            ImGui.Spacing();
            if (ImGui.Checkbox("Flash Light", ref Module.Visuals.Flashlight.IsEnabled))
            {
                Module.Visuals.Flashlight.State(Module.Visuals.Flashlight.IsEnabled);
            }
            ImGui.Spacing();
            ImGui.Checkbox("ThirdPersonView", ref Module.Visuals.ThirdPersonView.IsEnabled2);
        }
    }
}