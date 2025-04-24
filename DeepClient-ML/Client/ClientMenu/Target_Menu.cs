using System.Windows.Forms;
using DeepCore.Client.Misc;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using DeepCore.Client.Misc; // For AvatarExtensions
using VRC; // For VRC.Player

namespace DeepCore.Client.ClientMenu
{
    internal class Target_Menu
    {
        private static UiManager _uiManager;
        public static void InitMainMenu(UiManager UIManager)
        {
            _uiManager = UIManager;
            DeepConsole.Log("ClientUI", "Initializing Target Menu...");
            IButtonPage targetMenu = _uiManager.TargetMenu;
            UserFunctionMenu(UIManager);
            Module.WorldHacks.Murder4.Murder4TargetMenu(UIManager);
            Module.WorldHacks.AmongUs.AmongTargetMenu(UIManager);
        }
        public static void UserFunctionMenu(UiManager UIManager)
        {
            ReCategoryPage reCategory = UIManager.TargetMenu.AddCategoryPage("User functions");
            #region User Info
            reCategory.AddCategory("User Info");
            ReMenuCategory UserInfo = reCategory.GetCategory("User Info");
            UserInfo.AddButton("Copy Name", "Copy selected player name to you clipboard.", delegate
            {
                Clipboard.SetText(VrcExtensions.QM_GetSelectedUserName());
            });
            UserInfo.AddButton("Copy UserID", "Copy selected player avatar to you clipboard.", delegate
            {
                var a = ReMod.Core.VRChat.PlayerExtensions.GetVRCPlayer();
                Clipboard.SetText(a._player.prop_APIUser_0.id);
            });
            UserInfo.AddButton("Get avatar info", "Copy selected player avatar to you clipboard.", delegate
            {
                VrcExtensions.LogAvatar(ReMod.Core.VRChat.PlayerExtensions.GetVRCPlayer());
            });
            
            UserInfo.AddButton("Convert to pickup", "Converts the target avatar into a local pickup", delegate
            {
                var selectedPlayer = ReMod.Core.VRChat.PlayerExtensions.GetVRCPlayer();
                if (selectedPlayer != null)
                {
                    selectedPlayer._player.CloneAvatar(); // Using the extension method
                    VrcExtensions.Toast("DeepClient", $"Created pickup of {selectedPlayer._player.field_Private_APIUser_0.displayName}'s avatar");
                }
                else
                {
                    VrcExtensions.Toast("DeepClient", "No player selected!");
                }
            });
            
            UserInfo.AddButton("Delete all clones", "Removes all avatar pickups you created", delegate
            {
                int count = AvatarExtensions.AllClones.Count;
                AvatarExtensions.CleanupAllClones();
                VrcExtensions.Toast("DeepClient", $"Deleted {count} avatar clones");
            });
            
            
            #endregion
            #region User Exploits
            reCategory.AddCategory("User Exploits");
            ReMenuCategory UserExploits = reCategory.GetCategory("User Exploits");
            UserExploits.AddButton("Teleport", "Teleport to the selected user.", delegate
            {
                VrcExtensions.TpToPlayer(ReMod.Core.VRChat.PlayerExtensions.GetVRCPlayer());
            });
            UserExploits.AddButton("Silent\nTeleport", "Will teleport you on the target without them seeing you.", delegate
            {
                Module.Photon.MovementSerilize.State();
                VrcExtensions.Toast("DeepClient", "MovementSerilize enabled.");
                VrcExtensions.TpToPlayer(ReMod.Core.VRChat.PlayerExtensions.GetVRCPlayer());
            });
            UserExploits.AddButton("Force clone", "wtf ass clone.", delegate
            {
                var a = ReMod.Core.VRChat.PlayerExtensions.GetVRCPlayer();
                VrcExtensions.ChangeAvatar(a.field_Private_ApiAvatar_0.id);
            });
            UserExploits.AddButton("Force lewd", "Hoy voy a enseñar mis dos huevos y pene en directo", delegate
            {
                var a = ReMod.Core.VRChat.PlayerExtensions.GetVRCPlayer();
                Module.Exploits.ForceLewd.LewdPlayer(a);
            });
            UserExploits.AddButton("Reupload\nAvatar", "...", delegate
            {
                Module.Exploits.AviYoinker.askbeforeyoink();
            });
            UserExploits.AddToggle("Camera Sounds", "", delegate (bool s)
            {
                Module.Exploits.CamSoundSpammer.State(s);
            });
            UserExploits.AddToggle("Spy Camera", "Create a camera to spy on the player.", delegate (bool s)
            {
                Module.Exploits.SpyCamera.Toggle(s);
            });
            UserExploits.AddToggle("Hear Pleayer", "Allow you to hear the player when he is aways from you.", delegate (bool s)
            {
                VrcExtensions.ListenPlayer(ReMod.Core.VRChat.PlayerExtensions.GetVRCPlayer(), s);
            });
            UserExploits.AddToggle("Item Orbit", "Orbit player with items.", delegate (bool s)
            {
                Module.Exploits.ItemOrbit.State(s);
            });
            UserExploits.AddToggle("卍 Orbit", "Orbit player with 卍.", delegate (bool s)
            {
                Module.Exploits.SwasticaOrbit.State(s);
            });
            UserExploits.AddToggle("Seat OnHead", "Allows you to seat on current player head.", delegate (bool s)
            {
                Module.Movement.SeatOnHead.State(s);
            });
            #endregion
        }
    }
}
