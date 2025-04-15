using System;
using DeepCore.Client.Misc;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using VRC.Udon;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
    internal class UtilFunctions
    {
        public static void UtilFunctionsMenu(UiManager UIManager)
        {
            ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("Utils Functions", null);
            reCategoryPage.AddCategory("Functions");
            ReMenuCategory category = reCategoryPage.GetCategory("Functions");
            category.AddButton("Backup FrindIDs", "Allow you to bacups all of your frinds to a TXT.", delegate
            {
                ClientUtils.SaveFrinds();
            });
            category.AddButton("Russian Roulette", "Suka Blyat :3", delegate
            {
                Module.Funnies.RussianRoulette.RouletteStart();
            });
            category.AddButton("Log Udon", "Allow you to log all udon in a TXT.", delegate
            {
                ClientUtils.LogUdon();
            });
            category.AddButton("Dump\nAll\nUdon", "Allow you to dump all udon in a TXT.", delegate
            {
            foreach (UdonBehaviour udonBehaviour in UnityEngine.Object.FindObjectsOfType<UdonBehaviour>())
                {
                    UdonDisassembler.Disassemble(udonBehaviour,udonBehaviour.name);
                }
            });
            category.AddButton("Log VRCPickups", "Allow you to log all pickups in a TXT.", delegate
            {
                ClientUtils.LogItems();
            });
            category.AddButton("Clear Log", "Allow you to clear all log in debug/console.", delegate
            {
                Console.Clear();
                UI.QM.QMConsole.ClearLog();
            });
            category.AddToggle("HideShow QMConsole", "Allow you to show and hide qmconsole.", delegate (bool s)
            {
                UI.QM.QMConsole.ConsoleVisuabillity(s);
            },false);
            category.AddButton("Switch to DCAvatar", "Allow you to the client avi :fire:.", delegate
            {
                VrcExtensions.ChangeAvatar("avtr_14c10067-2a13-4d6c-8e38-d4359813af5a");
            });
        }
    }
}
