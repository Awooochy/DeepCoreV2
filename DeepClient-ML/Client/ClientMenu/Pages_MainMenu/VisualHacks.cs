using MelonLoader;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
    internal class VisualHacks
    {
        public static void VisualSettingsMenu(UiManager UIManager)
        {
            ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("Visuals Functions", null);
            reCategoryPage.AddCategory("InGame ESP");
            ReMenuCategory category = reCategoryPage.GetCategory("InGame ESP");
            category.AddToggle("Object ESP", "Allow you to see all pickups.", delegate (bool s)
            {
                Module.Visual.ESP.ObjectState(s);
            });
            category.AddToggle("Capsules ESP", "Allow you to see all players.", delegate (bool s)
            {
                Module.Visual.ESP.CapsuleState(s);
            });
            category.AddToggle("Udon ESP", "Allow you to see all udon.", delegate (bool s)
            {
                Module.Visual.ESP.UdonState(s);
            });
            category.AddToggle("Line ESP", "Allow you to see line from you to player.", delegate (bool s)
            {
                Module.Visual.LineESP.LineState(s);
            });
            category.AddToggle("Box ESP", "Allow you to see box from you to player.", delegate (bool s)
            {
                Module.Visual.BoxESP.BoxState(s);
            });
            reCategoryPage.AddCategory("OnGUI ESP");
            ReMenuCategory category1 = reCategoryPage.GetCategory("OnGUI ESP");
            category1.AddToggle("GUILine ESP", "Allow you to see line from you to player ongui.", delegate (bool s)
            {
                GUI.LineESP.LineState(s);
            });
            reCategoryPage.AddCategory("Others");
            ReMenuCategory category2 = reCategoryPage.GetCategory("Others");
            category2.AddToggle("Optifine Zoom", "Hold ALT to zoom.", delegate (bool s)
            {
                ConfManager.OptifineZoom.Value = s;
                MelonPreferences.Save();
            });
            category2.AddToggle("SelfHide", "Allow you to hide yourself (if you use crash avatar).", delegate (bool s)
            {
                Module.Visual.SelfHide.selfhidePlayer(s);
            });
            category2.AddToggle("Flashlight", "Allow you to see in the dark.", delegate (bool s)
            {
                Module.Visual.Flashlight.State(s);
            });
            category2.AddToggle("FlipScreen", "Allow you to rotate your pc cam by holding [R-CTRL + Scrolling].", delegate (bool s)
            {
                Module.Visual.FlipScreen.IsEnabled = s;
            });
        }
    }
}
