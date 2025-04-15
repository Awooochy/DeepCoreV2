using ReMod.Core.UI.QuickMenu;

namespace DeepCore.Client.Module.WorldHacks
{
    internal class PickupMenu
    {
        public static void PickupsMenu(ReMenuPage reCategoryPage)
        {
            ReCategoryPage reCategory = reCategoryPage.AddCategoryPage("Pickups Menu");
            reCategory.AddCategory("Exploits");
            ReMenuCategory PickupShit = reCategory.GetCategory("Exploits");
            PickupShit.AddToggle("Beyblade Pickups", "", delegate (bool s)
            {
                Exploits.BeyBladePickup.State(s);
            });
            PickupShit.AddToggle("Item 2 Click", "", delegate (bool s)
            {
                Movement.RayCastTP.Pickup2Click = s;
            });
            PickupShit.AddToggle("Pen Crash", "", delegate (bool s)
            {
                Exploits.PenClapper.State(s);
            });
            PickupShit.AddButton("Respawn All", "Respawn all pickups.", delegate ()
            {
                PickupUtils.Respawn();
            }, null, "#ffffff");
            PickupShit.AddButton("Bring to me", "Bring all pickups to you.", delegate ()
            {
                PickupUtils.BringPickups();
            }, null, "#ffffff");
        }
    }
}
