using BepInEx;
using BepInEx.Configuration;
using System.IO;

namespace DeepClient.Client
{
    internal class ConfManager
    {
        private static ConfigFile configFile;
        public static ConfigEntry<bool> OptifineZoom;
        public static ConfigEntry<bool> udonLogger;
        public static ConfigEntry<bool> antiUdon;
        public static ConfigEntry<bool> blockWorldTriggers;
        public static ConfigEntry<bool> avatarLogging;
        public static ConfigEntry<bool> antiInvalidRPC;
        public static ConfigEntry<bool> playerLogger;
        public static ConfigEntry<bool> VRCAdminStaffLogger;
        public static ConfigEntry<bool> customnameplate;
        public static ConfigEntry<bool> OwnerSpoof;
        public static ConfigEntry<bool> AntiQuest;
        public static ConfigEntry<bool> AntiAvatarScallingdisabler;
        public static ConfigEntry<bool> VideoPlayerURLLogger;
        public static ConfigEntry<bool> ShouldQMFreeze;
        public static ConfigEntry<bool> ShouldMenuMusic;
        public static ConfigEntry<bool> ShouldSteamAPI;
        public static ConfigEntry<bool> fakePingEnabled;
        public static ConfigEntry<bool> fakeFPSEnabled;
        public static ConfigEntry<bool> JoinLastInstance;
        public static ConfigEntry<bool> BLShowLoadingMessages;
        public static ConfigEntry<bool> BLSWarpTunnel;
        public static ConfigEntry<bool> BLSVrcLogo;
        public static ConfigEntry<bool> BLSModSounds;
        public static ConfigEntry<bool> BLSEnabled;
        public static ConfigEntry<bool> FlyKeyBind;
        public static ConfigEntry<bool> ThirdPersonKeyBind;
        public static ConfigEntry<bool> ChatBoxLogger;
        public static ConfigEntry<float> flySpeedValue;
        public static ConfigEntry<float> fakeFPS;
        public static ConfigEntry<int> maxAvatarLogToFile;
        public static ConfigEntry<int> fakePing;
        public static ConfigEntry<string> E1Payload;
        public static ConfigEntry<string> LastInstanceID;
        public static ConfigEntry<string> resourcePath;
        public static void Initialize()
        {
            string configPath = Path.Combine(Paths.ConfigPath, "DeepClient.cfg");
            configFile = new ConfigFile(configPath, true);
            OptifineZoom = configFile.Bind("General", "OptifineZoom", false, "Enable Optifine zoom.");
            udonLogger = configFile.Bind("General", "udonLogger", false, "Enable Udon Logger.");
            antiUdon = configFile.Bind("General", "antiUdon", false, "Enable anti-Udon.");
            blockWorldTriggers = configFile.Bind("General", "blockWorldTriggers", true, "Block World Triggers.");
            avatarLogging = configFile.Bind("General", "avatarLogging", false, "Enable Avatar Logging.");
            antiInvalidRPC = configFile.Bind("General", "antiInvalidRPC", false, "Enable anti Invalid RPC.");
            playerLogger = configFile.Bind("General", "playerLogger", false, "Enable Player Logger.");
            VRCAdminStaffLogger = configFile.Bind("General", "VRCAdminStaffLogger", false, "Enable VRC Admin Staff Logger.");
            customnameplate = configFile.Bind("General", "customnameplate", false, "Enable Custom Nameplate.");
            OwnerSpoof = configFile.Bind("General", "OwnerSpoof", false, "Enable Owner Spoof.");
            AntiQuest = configFile.Bind("General", "AntiQuest", false, "Enable Anti-Quest.");
            AntiAvatarScallingdisabler = configFile.Bind("General", "AntiAvatarScallingdisabler", false, "Disable Avatar Scaling.");
            VideoPlayerURLLogger = configFile.Bind("General", "VideoPlayerURLLogger", false, "Enable Video Player URL Logger.");
            ShouldQMFreeze = configFile.Bind("General", "ShouldQMFreeze", false, "Enable QM Freeze.");
            ShouldMenuMusic = configFile.Bind("General", "ShouldMenuMusic", false, "Enable Menu Music.");
            ShouldSteamAPI = configFile.Bind("General", "ShouldSteamAPI", false, "Enable Steam API.");
            fakePingEnabled = configFile.Bind("General", "fakePingEnabled", false, "Enable Fake Ping.");
            fakeFPSEnabled = configFile.Bind("General", "fakeFPSEnabled", false, "Enable Fake FPS.");
            JoinLastInstance = configFile.Bind("General", "JoinLastInstance", false, "Join the last instance.");
            ChatBoxLogger = configFile.Bind("General", "ChatBoxLogger", false, "Enable ChatBoxLogger.");
            BLShowLoadingMessages = configFile.Bind("General", "BLShowLoadingMessages", true, "Show Loading Messages.");
            BLSWarpTunnel = configFile.Bind("General", "BLSWarpTunnel", true, "Enable BLS Warp Tunnel.");
            BLSVrcLogo = configFile.Bind("General", "BLSVrcLogo", true, "Enable BLS VRC Logo.");
            BLSModSounds = configFile.Bind("General", "BLSModSounds", true, "Enable BLS Mod Sounds.");
            BLSEnabled = configFile.Bind("General", "BLSEnabled", true, "Enable BLS.");
            FlyKeyBind = configFile.Bind("KeyBinds", "FlyKeyBind", false, "Enable Fly Keybind.");
            ThirdPersonKeyBind = configFile.Bind("KeyBinds", "ThirdPersonKeyBind", false, "Enable Third-Person Keybind.");
            ChatBoxLogger = configFile.Bind("KeyBinds", "ThirdPersonKeyBind", false, "Enable Third-Person Keybind.");
            flySpeedValue = configFile.Bind("General", "flySpeedValue", 10f, "Fly Speed Value.");
            fakeFPS = configFile.Bind("General", "fakeFPS", 60f, "Fake FPS.");
            maxAvatarLogToFile = configFile.Bind("General", "maxAvatarLogToFile", 100, "Max Avatar Log to File.");
            fakePing = configFile.Bind("General", "fakePing", 100, "Fake Ping Value.");
            E1Payload = configFile.Bind("General", "E1Payload", "default", "E1 Payload.");
            LastInstanceID = configFile.Bind("General", "LastInstanceID", "None", "Last Instance ID.");
            resourcePath = configFile.Bind("General", "resourcePath", "defaultPath", "Resource Path.");
            DeepConsole.Log("Config", "Config initialized.");
        }
        public static void SaveConfig()
        {
            configFile.Save();
            DeepConsole.LogConsole("Config","Settings saved.");
        }
    }
}