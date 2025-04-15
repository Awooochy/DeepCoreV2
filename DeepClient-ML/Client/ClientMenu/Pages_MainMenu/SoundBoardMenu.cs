using DeepCore.Client.Module.QOL;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
    internal class SoundBoardMenu
    {
        public static void SBMenu(UiManager UIManager)
        {
            ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("Networking SoundBoard", null);
            reCategoryPage.AddCategory("Available Sounds");
            ReMenuCategory category = reCategoryPage.GetCategory("Available Sounds");
            category.AddButton("Capybara", "", delegate
            {
                UdonSoundBoard.SendSound("DCCapybara");
            });
            category.AddButton("Test", "", delegate
            {
                UdonSoundBoard.SendSound("DC");
            });
        }
    }
}
