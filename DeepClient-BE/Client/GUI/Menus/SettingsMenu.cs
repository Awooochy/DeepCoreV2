using ImGuiNET;

namespace DeepClient.Client.GUI.Menus
{
    internal class SettingsMenu
    {
        public static void Render()
        {
            if (ImGui.CollapsingHeader("Client Settings"))
            {
                if (ImGui.Button("Player Logger"))
                {
                    ConfManager.playerLogger.Value = !ConfManager.playerLogger.Value;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ConfManager.playerLogger.Value ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), ConfManager.playerLogger.Value ? "Player Logger: On" : "Player Logger: Off");
                    ImGui.TextDisabled("Log Join/Leave.");
                    ImGui.EndTooltip();
                }
                ImGui.SameLine();
                if (ImGui.Button("Staff Logger"))
                {
                    ConfManager.VRCAdminStaffLogger.Value = !ConfManager.VRCAdminStaffLogger.Value;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ConfManager.VRCAdminStaffLogger.Value ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), ConfManager.VRCAdminStaffLogger.Value ? "Staff Logger: On" : "Staff Logger: Off");
                    ImGui.TextDisabled("Log VRCAdmin/Staff Join.");
                    ImGui.EndTooltip();
                }
                ImGui.SameLine();
                if (ImGui.Button("Avatar Logger"))
                {
                    ConfManager.avatarLogging.Value = !ConfManager.avatarLogging.Value;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ConfManager.avatarLogging.Value ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), ConfManager.avatarLogging.Value ? "Avatar Logger: On" : "Avatar Logger: Off");
                    ImGui.TextDisabled("Toggle avatar logging on/off.");
                    ImGui.EndTooltip();
                }
                ImGui.SameLine();
                if (ImGui.Button("UdonSync Logger"))
                {
                    ConfManager.udonLogger.Value = !ConfManager.udonLogger.Value;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ConfManager.udonLogger.Value ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), ConfManager.udonLogger.Value ? "Udon Logger: On" : "Udon Logger: Off");
                    ImGui.TextDisabled("Toggle udon logging on/off.");
                    ImGui.EndTooltip();
                }
                ImGui.Spacing();
                if (ImGui.Button("AntiQuest"))
                {
                    ConfManager.AntiQuest.Value = !ConfManager.AntiQuest.Value;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ConfManager.AntiQuest.Value ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), ConfManager.AntiQuest.Value ? "AntiQuest: On" : "AntiQuest: Off");
                    ImGui.TextDisabled("Auto block quest users.");
                    ImGui.EndTooltip();
                }
                ImGui.SameLine();
                if (ImGui.Button("NoAvatarScalling"))
                {
                    ConfManager.AntiAvatarScallingdisabler.Value = !ConfManager.AntiAvatarScallingdisabler.Value;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ConfManager.AntiAvatarScallingdisabler.Value ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), ConfManager.AntiAvatarScallingdisabler.Value ? "AntiQuest: On" : "AntiQuest: Off");
                    ImGui.TextDisabled("Block disable avatar scalling.");
                    ImGui.EndTooltip();
                }
                ImGui.SameLine();
                if (ImGui.Button("QMFreeze"))
                {
                    ConfManager.ShouldQMFreeze.Value = !ConfManager.ShouldQMFreeze.Value;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ConfManager.ShouldQMFreeze.Value ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), ConfManager.ShouldQMFreeze.Value ? "QMFreeze: On" : "QMFreeze: Off");
                    ImGui.TextDisabled("Allow you to lock yourself when opening menu when falling down.");
                    ImGui.EndTooltip();
                }
                ImGui.SameLine();
                if (ImGui.Button("Menu Music"))
                {
                    ConfManager.ShouldMenuMusic.Value = !ConfManager.ShouldMenuMusic.Value;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ConfManager.ShouldMenuMusic.Value ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), ConfManager.ShouldMenuMusic.Value ? "MenuMusic: On" : "MenuMusic: Off");
                    ImGui.TextDisabled("Music or not? (Relauch of the game needed).");
                    ImGui.EndTooltip();
                }
                ImGui.Spacing();
                if (ImGui.Button("Custom E1 Payload"))
                {
                    Module.Exploits.EarRape.SetPayload();
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextDisabled("Allows you to custom e1 payloads.");
                    ImGui.EndTooltip();
                }
                ImGui.SameLine();
                if (ImGui.Button("CustomPlate"))
                {
                    ConfManager.customnameplate.Value = !ConfManager.customnameplate.Value;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ConfManager.customnameplate.Value ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), ConfManager.customnameplate.Value ? "CustomNameplate: On" : "CustomNameplate: Off");
                    ImGui.TextDisabled("Allow CustomNamePlate.");
                    ImGui.EndTooltip();
                }
                ImGui.SameLine();
                if (ImGui.Button("Save LastInstance"))
                {
                    ConfManager.JoinLastInstance.Value = !ConfManager.JoinLastInstance.Value;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ConfManager.JoinLastInstance.Value ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), ConfManager.JoinLastInstance.Value ? "SaveLastInstance: On" : "SaveLastInstance: Off");
                    ImGui.TextDisabled("Allow you to save lastinstance you where in.");
                    ImGui.EndTooltip();
                }
                ImGui.SameLine();
                if (ImGui.Button("BetterLoadingScreen"))
                {
                    ConfManager.BLSEnabled.Value = !ConfManager.BLSEnabled.Value;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ConfManager.BLSEnabled.Value ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), ConfManager.BLSEnabled.Value ? "BetterLoadingScreen: On" : "BetterLoadingScreen: Off");
                    ImGui.TextDisabled("Allow you to toggle BLS (Restart Needed).");
                    ImGui.EndTooltip();
                }
                ImGui.SameLine();
                if (ImGui.Button("OwnerSpoof"))
                {
                    ConfManager.OwnerSpoof.Value = !ConfManager.OwnerSpoof.Value;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ConfManager.OwnerSpoof.Value ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), ConfManager.OwnerSpoof.Value ? "OwnerSpoof: On" : "OwnerSpoof: Off");
                    ImGui.TextDisabled("Spoofer your username as the world auther.");
                    ImGui.EndTooltip();
                }
            }
            if (ImGui.CollapsingHeader("Photon Settings"))
            {
                if (ImGui.Button("Event 43"))
                {
                    ConfManager.ChatBoxLogger.Value = !ConfManager.ChatBoxLogger.Value;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ConfManager.ChatBoxLogger.Value ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), ConfManager.ChatBoxLogger.Value ? "ChatBoxLogger: On" : "ChatBoxLogger: Off");
                    ImGui.TextDisabled("Log chatbox msg.");
                    ImGui.EndTooltip();
                }
                ImGui.SameLine();
                if (ImGui.Button("Log OnEvent"))
                {
                    Patching.Modules.LoadBalancingClienta.isdebugtime = !Patching.Modules.LoadBalancingClienta.isdebugtime;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(Patching.Modules.LoadBalancingClienta.isdebugtime ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), Patching.Modules.LoadBalancingClienta.isdebugtime ? "OnEvent: On" : "OnEvent: Off");
                    ImGui.TextDisabled("Log photon onevent to cmd.");
                    ImGui.EndTooltip();
                }
            }
            if (ImGui.CollapsingHeader("KeyBinds Settings"))
            {
                if (ImGui.Button("Fly Keybind"))
                {
                    ConfManager.FlyKeyBind.Value = !ConfManager.FlyKeyBind.Value;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ConfManager.FlyKeyBind.Value ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), ConfManager.FlyKeyBind.Value ? "FlyKeybind: On" : "FlyKeybindt: Off");
                    ImGui.TextDisabled("Allow you to toggle flight with (R-CTRL + F).");
                    ImGui.EndTooltip();
                }
                ImGui.SameLine();
                if (ImGui.Button("ThirdPerson Keybind"))
                {
                    ConfManager.ThirdPersonKeyBind.Value = !ConfManager.ThirdPersonKeyBind.Value;
                }
                if (ImGui.IsItemHovered())
                {
                    ImGui.BeginTooltip();
                    ImGui.TextColored(ConfManager.ThirdPersonKeyBind.Value ? new System.Numerics.Vector4(0f, 1f, 0f, 1f) : new System.Numerics.Vector4(1f, 0f, 0f, 1f), ConfManager.ThirdPersonKeyBind.Value ? "ThirdPersonKeybind: On" : "ThirdPersonKeybind: Off");
                    ImGui.TextDisabled("Allow you to toggle flight with (R-CTRL + F).");
                    ImGui.EndTooltip();
                }
            }
            ImGui.Spacing();
            ImGui.Text("For the love of god, please press this button below.");
            if (ImGui.Button("Save Config"))
            {
                ConfManager.SaveConfig();
            }
        }
    }
}
