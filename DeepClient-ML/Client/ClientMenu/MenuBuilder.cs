using ReMod.Core.Managers;

namespace DeepCore.Client.ClientMenu
{
    public class MenuBuilder
    {
        internal static UiManager _uiManager;
        internal static void MenuStart()
        {
            DeepConsole.Log("ClientUI","Initializing UI...");
            _uiManager = new UiManager("DeepClient",Misc.SpriteManager.clientIcon, true, true);
            LaunchPad_Menu.InitLaunchPadMenu(_uiManager);
            Target_Menu.InitMainMenu(_uiManager);
            Main_Menu.InitMainMenu(_uiManager);
        }
    }
}
