using DeepCore.Client.ClientMenu.Pages_MainMenu;
using ReMod.Core.Managers;

namespace DeepCore.Client.ClientMenu
{
    internal class Main_Menu
    {
        private static UiManager _uiManager;
        public static void InitMainMenu(UiManager UIManager)
        {
            _uiManager = UIManager;
            DeepConsole.Log("ClientUI","Initializing Main Menu...");
            VisualHacks.VisualSettingsMenu(_uiManager);
            MovementHacks.MovementHacksMenu(_uiManager);
            Module.WorldHacks.WorldHackMainMenu.WorldHacksMenu(_uiManager);
            ExploitHacks.ExploitHacksMenu(_uiManager);
            UtilFunctions.UtilFunctionsMenu(_uiManager);
            LastSeenAvatars.LoggedAvatarsMenu(_uiManager);
            InstaceHistory.InstaceHistoryHacksMenu(_uiManager);
            ItemManipulator.ManipulatorHacksMenu(_uiManager);
            MediaControl.MediaControlMenu(_uiManager);
            ClientChat.ClientChatMenu(_uiManager);
            ClientSettings.ClientSettingsMenu(_uiManager);
        }
        public static void AddSpacers()
        {
            for (int i = 0; i < 8; i++)
            {
                _uiManager.QMMenu.AddSpacer();
            }
        }
    }
}
