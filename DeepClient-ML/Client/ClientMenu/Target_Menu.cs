using System.Windows.Forms;
using DeepCore.Client.Misc;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;

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
            UserExploits.AddButton("Force lewd", "...", delegate
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
