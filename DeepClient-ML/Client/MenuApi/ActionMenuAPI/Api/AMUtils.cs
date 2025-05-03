namespace AstroClient.MenuApi.ActionMenuAPI.Api
{
    using System;
    using Helpers;
    using Managers;
    using MelonLoader;
    using UnityEngine;
    using WorldModifications.WorldsIds;

    /// <summary>
    /// General Action Menu Things
    /// </summary>
    public static class AMUtils
    {
        /// <summary>
        ///     Trigger a refresh for the action menus
        /// </summary>
        public static void RefreshActionMenu()
        {
            try
            {
                Utilities.RefreshAM();
            }
            catch (Exception e)
            {
                Log.Warn(
                    $"Refresh failed (oops). This may or may not be an oof if another exception immediately follows after this exception: {e}");
                //This is semi-abusable if this fails so its probably a good idea to have a fail-safe to protect sensitive functions that are meant to be locked
                Utilities.ResetMenu();
            }
        }

        /// <summary>
        ///     Add a mod to a dedicated section of the action menu with other mods
        /// Toggles the acitve/playing state of a pedal without notifying the event listeners.
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="openFunc">
        ///     Function called when your mod page is opened. Add your methods calls to other AMAPI methods such
        ///     AddRadialPedalToSubMenu to add buttons to the submenu it creates when clicked
        /// </param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="ShowMenu">(optional) Show It or not</param>
        public static void AddToModsFolder_Cheats(string text, Action<CustomSubMenu> openFunc, Texture2D icon = null,
            bool showMenu = true)
        {

            ModsFolderManager.AddMod((submenu) =>
            {
                if (WorldIdentifier.ShowWorldCheatActionMenu)
                {
                    submenu.AddSubMenu(text, openFunc, icon, false);
                }
            });
        }

        /// <summary>
        ///     Trigger a complete reset for the action menus
        /// </summary>
        public static void ResetMenu()
        {
            Utilities.ResetMenu();
        }

        /// <summary>
        ///     Add a mod to a dedicated section of the action menu with other mods
        /// </summary>
        /// <param name="text">Button text</param>
        /// <param name="openFunc">
        ///     Function called when your mod page is opened. Add your methods calls to other AMAPI methods such
        ///     AddRadialPedalToSubMenu to add buttons to the submenu it creates when clicked
        /// </param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="locked">(optional) Starting state of pedal</param>
        public static void AddToModsFolder(string text, Action<CustomSubMenu> openFunc, Texture2D icon = null)
        {
            ModsFolderManager.AddMod(subMenu => { subMenu.AddSubMenu(text, openFunc, icon, false); });
        }

        /// <summary>
        /// Toggles the acitve/playing state of a pedal without notifying the event listeners.
        /// </summary>
        public static void SetActiveNoCallback(this PedalOption pedalOption, bool active) {
            pedalOption.SetPedalTypeIcon(
                active ? Utilities.GetExpressionsIcons().typePlayOn : Utilities.GetExpressionsIcons().typePlayOff);
            pedalOption.SetPlaying(active);
        }
    }
}