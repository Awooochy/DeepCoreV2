namespace AstroClient.MenuApi.ActionMenuAPI.Managers
{
    using System;
    using System.Collections.Generic;
    using Api;
    using Helpers;

    internal static class ModsFolderManager
    {
        public static List<Action<CustomSubMenu>> mods = new();
        public static List<List<Action<CustomSubMenu>>> splitMods;

        private static readonly Action<CustomSubMenu> openFunc = subMenu =>
        {
            if (mods.Count <= Constants.MAX_PEDALS_PER_PAGE)
            {
                foreach (var action in mods) action(subMenu);
            }
            else
            {
                if (splitMods == null) splitMods = mods.Split(Constants.MAX_PEDALS_PER_PAGE);
                for (var i = 0; i < splitMods.Count && i < Constants.MAX_PEDALS_PER_PAGE; i++)
                {
                    var index = i;
                    subMenu.AddSubMenu($"Page {i + 1}", subMenu =>
                    {
                        foreach (var action in splitMods[index]) action(subMenu);
                    }, ResourcesManager.GetPageIcon(i + 1));
                }
            }
        };

        public static void AddMod(Action<CustomSubMenu> openingAction)
        {
            mods.Add(openingAction);
        }

        /*public void RemoveMod(Action openingAction)
        {
            mods.Remove(openingAction);
        }*/

        public static void AddMainPageButton(ActionMenu menu)
        {
            new CustomSubMenu(menu)
                .AddSubMenu(Constants.MODS_FOLDER_NAME, openFunc, ResourcesManager.GetModsSectionIcon());
        }
    }
}