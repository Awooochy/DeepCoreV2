using System.Collections;
using MelonLoader;
using ReMod.Core.UI.QuickMenu;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace DeepCore.Client.Module.WorldHacks
{
    internal class TowerDefence
    {
        internal static bool RegenLoopState;

        public static void Menu(ReMenuPage reCategoryPage)
        {
            ReMenuPage reCategory = reCategoryPage.AddMenuPage("Tower Defence");
            reCategory.AddToggle("God Mode", "", delegate (bool s)
            {
                RegenLoopState = s;
                if (s)
                {
                    MelonCoroutines.Start(RegenLoop());
                }
            });
            reCategory.AddButton("+ Money", "MoneyHackLoaded (Make sure you have a tower placed down).", MoneyHackLoaded);
            reCategory.AddButton("Regenerate Health", "Give new life.", RegenLife);
            reCategory.AddButton("Create Last tower", "Creates the last tower you placed down (do not do if you dont have a tower placed down).", CreateLastTower);
            reCategory.AddButton("Upgrade Last Tower", "Upgrades the last tower you placed down.", UpgradeLastTower);
            reCategory.AddButton("Reset All Towers", "Clears all you towers.", ResetAllTower);
        }
        internal static IEnumerator RegenLoop()
        {
            do
            {
                GameObject.Find("HealthController").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Revive");
                yield return new WaitForSeconds(2f);
            }
            while (RegenLoopState);
            yield break;
        }
        public static void MoneyHackLoaded()
        {
            GameObject.Find("TowerManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "CreateTower");
            GameObject.Find("TowerManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "TrySellTower");
        }
        public static void RegenLife()
        {
            GameObject.Find("HealthController").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Revive");
        }
        public static void CreateLastTower()
        {
            GameObject.Find("TowerManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "CreateTower");
        }
        public static void UpgradeLastTower()
        {
            GameObject.Find("TowerManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "TryUpgradeTower");
        }
        public static void ResetAllTower()
        {
            GameObject.Find("TowerManager").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "ClearTowers");
        }

    }
}
