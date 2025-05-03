using System.IO;
using DeepCore.Client;
using MelonLoader;

namespace DeepCore
{
    internal class ConfManager
    {
        internal static MelonPreferences_Entry<bool> OptifineZoom;
        internal static MelonPreferences_Entry<bool> udonLogger;
        internal static MelonPreferences_Entry<bool> antiUdon;
        internal static MelonPreferences_Entry<bool> blockWorldTriggers;
        internal static MelonPreferences_Entry<bool> avatarLogging;
        internal static MelonPreferences_Entry<bool> antiInvalidRPC;
        internal static MelonPreferences_Entry<bool> playerLogger;
        internal static MelonPreferences_Entry<bool> VRCAdminStaffLogger;
        internal static MelonPreferences_Entry<bool> customnameplate;
        internal static MelonPreferences_Entry<bool> OwnerSpoof;
        internal static MelonPreferences_Entry<bool> AntiQuest;
        internal static MelonPreferences_Entry<bool> AntiAvatarScallingdisabler;
        internal static MelonPreferences_Entry<bool> VideoPlayerURLLogger;
        internal static MelonPreferences_Entry<bool> ShouldMenuMusic;
        internal static MelonPreferences_Entry<bool> ShouldSteamAPI;
        internal static MelonPreferences_Entry<bool> fakePingEnabled;
        internal static MelonPreferences_Entry<bool> fakeFPSEnabled;
        internal static MelonPreferences_Entry<float> flySpeedValue;
        internal static MelonPreferences_Entry<int> maxAvatarLogToFile;
        internal static MelonPreferences_Entry<int> fakePing;
        internal static MelonPreferences_Entry<float> fakeFPS;
        internal static MelonPreferences_Entry<string> LastInstanceID;
        internal static MelonPreferences_Entry<bool> JoinLastInstance;
        public static MelonPreferences_Entry<bool> BLShowLoadingMessages;
        public static MelonPreferences_Entry<bool> BLSWarpTunnel;
        public static MelonPreferences_Entry<bool> BLSVrcLogo;
        public static MelonPreferences_Entry<bool> BLSModSounds;
        public static MelonPreferences_Entry<bool> BLSEnabled;
        #region KeyBinds
        internal static MelonPreferences_Entry<bool> FlyKeyBind;
        internal static MelonPreferences_Entry<bool> DoubleFlyKeyBind;
        internal static MelonPreferences_Entry<bool> ThirdPersonKeyBind;
        internal static MelonPreferences_Entry<bool> SerializeKeyBind;
        #endregion
        private static MelonPreferences_Entry<string> resourcePath;
        public static void FolderCheck()
        {
            string folderPath = "DeepCoreV2";
            if (Directory.Exists(folderPath))
            {
            }
            else
            {
                Directory.CreateDirectory(folderPath);
                DeepConsole.Log("Config",$"Created {folderPath} folder.");
            }
            if (Directory.Exists(folderPath+"/LoadingMusic"))
            {
            }
            else
            {
                Directory.CreateDirectory(folderPath + "/LoadingMusic");
                DeepConsole.Log("Config", $"Created {folderPath + "/LoadingMusic"} folder.");
            }
            if (Directory.Exists(folderPath + "/AssetBundles"))
            {
            }
            else
            {
                Directory.CreateDirectory(folderPath + "/AssetBundles");
                DeepConsole.Log("Config", $"Created {folderPath + "/AssetBundles"} folder.");
            }
        }
        public static void initConfig()
        {
            DeepConsole.Log("Config","Initializing Config...");
            
            FolderCheck();
            
            MelonPreferences_Category melonPreferences_Category = MelonPreferences.CreateCategory("DeepCoreV2", "DeepCoreV2");
            
            
            OptifineZoom = melonPreferences_Category.CreateEntry<bool>("optifinezoom", false, "Allow you to zoom like opf when hold alt.", null, false, false, null, null);
            
            antiUdon = melonPreferences_Category.CreateEntry<bool>("antiUdon", false, "Block Udon Events", null, false, false, null, null);
            
            blockWorldTriggers = melonPreferences_Category.CreateEntry<bool>("blockWorldTriggers", false, "Block WorldTriggers things.", null, false, false, null, null);
            
            avatarLogging = melonPreferences_Category.CreateEntry<bool>("avatarLogging", false, "Log Avatar Change.", null, false, false, null, null);
            
            antiInvalidRPC = melonPreferences_Category.CreateEntry<bool>("antiInvalidRPC", false, "Blocks Invalid RPC.", null, false, false, null, null);
            
            playerLogger = melonPreferences_Category.CreateEntry<bool>("playerLogger", false, "Log Player Join/Leave Events.", null, false, false, null, null);
            
            VRCAdminStaffLogger = melonPreferences_Category.CreateEntry<bool>("vrcadminstafflogger", false, "Log AdminStaff Join/Leave.", null, false, false, null, null);
            
            udonLogger = melonPreferences_Category.CreateEntry<bool>("udonLogger", false, "Log Udon Events", null, false, false, null, null);
            
            customnameplate = melonPreferences_Category.CreateEntry<bool>("customnameplate", false, "Allow you to see player FPS/PING/Platform.", null, false, false, null, null);
            
            OwnerSpoof = melonPreferences_Category.CreateEntry<bool>("ownerspoof", false, "Allow you to spoof your username as the world owner.", null, false, false, null, null);
            
            AntiQuest = melonPreferences_Category.CreateEntry<bool>("antiquest", false, "auto block quest user.", null, false, false, null, null);
            
            AntiAvatarScallingdisabler = melonPreferences_Category.CreateEntry<bool>("AntiAvatarScallingdisabler", false, "bloack avatar scalling.", null, false, false, null, null);
            
            VideoPlayerURLLogger = melonPreferences_Category.CreateEntry<bool>("VideoPlayerURLLogger", false, "Log loading utl.", null, false, false, null, null);
            
            
            ShouldMenuMusic = melonPreferences_Category.CreateEntry<bool>("MenuMusic", false, "Allow you to have a music when opening your menu..", null, false, false, null, null);
            
            
            ShouldSteamAPI = melonPreferences_Category.CreateEntry<bool>("SteamAPI", false, "Allow you to disabled steamapi.", null, false, false, null, null);
            
            
            flySpeedValue = melonPreferences_Category.CreateEntry<float>("flySpeedValue", 8f, "Fly Speed Value.", null, false, false, null, null);
            
            
            maxAvatarLogToFile = melonPreferences_Category.CreateEntry<int>("maxAvatarLogToFile", 96, "Max avatar entries for avatar file logging.", null, false, false, null, null);
            
            
            fakePing = melonPreferences_Category.CreateEntry<int>("fakePing", 30, "Fake Ping Value", null, false, false, null, null);
            
            
            fakeFPS = melonPreferences_Category.CreateEntry<float>("fakeFPS", 80f, "Fake FPS Value", null, false, false, null, null);
            
            
            fakePingEnabled = melonPreferences_Category.CreateEntry<bool>("fakePingEnabled", false, "Fake Ping Enabled", null, false, false, null, null);
            
            
            fakeFPSEnabled = melonPreferences_Category.CreateEntry<bool>("fakeFPSEnabled", false, "Fake FPS Enabled", null, false, false, null, null);
            
            
            
            LastInstanceID = melonPreferences_Category.CreateEntry<string>("LastInstanceID", "", "Last Instance you did join.", null, false, false, null, null);
            
            
            JoinLastInstance = melonPreferences_Category.CreateEntry<bool>("JoinLastInstance", false, "Allow you to rejoin last instance you where in.", null, false, false, null, null);
            
            
            #region Keybiinds
            
            FlyKeyBind = melonPreferences_Category.CreateEntry<bool>("FlyKeyBind", false, "Allow you to use ctrl and f for toggling flight.", null, false, false, null, null);
            
            
            DoubleFlyKeyBind = melonPreferences_Category.CreateEntry<bool>("DoubleFlyKeyBind", false, "Allow you to use ctrl and f for toggling flight.", null, false, false, null, null);
            
            
            ThirdPersonKeyBind = melonPreferences_Category.CreateEntry<bool>("ThirdPersonKeyBind", false, "Allow you to use ThirdPerson keybind.", null, false, false, null, null);
            
            
            SerializeKeyBind = melonPreferences_Category.CreateEntry<bool>("SerializeKeyBind", false, "Allow you to toggle serialize shit with '~'.", null, false, false, null, null);
            #endregion
            
            
            
            BLSEnabled = melonPreferences_Category.CreateEntry("BLSEnabled", false, "BLSEnabled. (Enable for LoadingScreenPictures compatibility)");
            
            
            BLShowLoadingMessages = melonPreferences_Category.CreateEntry("LoadingMessages", false, "Show loading messages. (Enable for LoadingScreenPictures compatibility)");
            
            
            BLSWarpTunnel = melonPreferences_Category.CreateEntry("Warp Tunnel", true, "Toggle warp tunnel (good for reducing motion)");
            
            
            BLSVrcLogo = melonPreferences_Category.CreateEntry("Vrchat Logo", true, "Toggle VRChat logo");
            
            
            BLSModSounds = melonPreferences_Category.CreateEntry("Mod Sounds", true, "Toggle mod music");
            
            
            resourcePath = melonPreferences_Category.CreateEntry<string>("resourcePath", "DeepCoreV2", "Location for Folder.", null, false, false, null, null);
            
            
            melonPreferences_Category.SetFilePath(getResourcePathFull() + "//DeepConfig.cfg");
            
            
            melonPreferences_Category.SaveToFile(true);
            
        }
        internal static string getResourcePathFull()
        {
            return Path.Combine("DeepCoreV2");
        }
    }
}