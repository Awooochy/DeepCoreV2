using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;

namespace DeepCore.Client.Module.WorldHacks
{
    internal class WorldHackMainMenu
    {
        public static void WorldHacksMenu(UiManager UIManager)
        {
            ReMenuPage reCategoryPage = UIManager.QMMenu.AddMenuPage("WorldHacks Functions", null);
            AmongUs.AmongusMenu(reCategoryPage);
            Murder4.Murder4Menu(reCategoryPage);
            JustBClub3.JBC3Menu(reCategoryPage);
            PickupMenu.PickupsMenu(reCategoryPage);
        }
    }
}
