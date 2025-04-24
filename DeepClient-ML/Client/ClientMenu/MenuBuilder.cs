using ReMod.Core.Managers;
using DeepCore.Client.Misc;

namespace DeepCore.Client.ClientMenu
{
    public class MenuBuilder
    {
        internal static UiManager _uiManager;
        
        internal static void MenuStart()
        {
            // Ensure sprites are loaded first
            SpriteManager.LoadAllSprites();
            
            DeepConsole.Log("ClientUI", "Initializing UI...");
            
            // Use the public property ClientIcon instead of the old field clientIcon
            _uiManager = new UiManager(
                "DeepCoreV2",
                SpriteManager.ClientIcon,  // Using the property with proper capitalization
                true, 
                true
            );
            
            // Initialize menus
            LaunchPad_Menu.InitLaunchPadMenu(_uiManager);
            Target_Menu.InitMainMenu(_uiManager);
            Main_Menu.InitMainMenu(_uiManager);
        }
    }
}