using System.Collections;
using UnityEngine;

namespace DeepCore.Client.Module.WorldHacks
{
    internal class BlackCat
    {
        public static IEnumerator InitTheme()
        {
            while (GameObject.Find("UI") == null)
            {
                yield return null;
            }
            DeepConsole.LogConsole("BL", "Disabling useless shit...");
            if (GameObject.Find("pin_pedastal"))
            {
                GameObject.Find("pin_pedastal").SetActive(false);
            }
            if (GameObject.Find("UI/Pins merch canvas"))
            {
                GameObject.Find("UI/Pins merch canvas").SetActive(false);
            }
            if (GameObject.Find("UI/Pins merch canvas (1)"))
            {
                GameObject.Find("UI/Pins merch canvas (1)").SetActive(false);
            }
            DeepConsole.LogConsole("BL", "Enabling GoodShit...");
            GameObjectToggle("patreon Toggles", true);
            GameObjectToggle("Sync Button", true);
            GameObjectToggle("Buy", false);
            GameObjectToggle("Owned", true);
            while (GameObject.Find("CREATOR ECONOMY") == null)
            {
                yield return null;
            }
            GameObject.Find("CREATOR ECONOMY/ITEMS/MOVEMENT Canvas").SetActive(true);
            yield break;
        }
        public static void GameObjectToggle(string gameObjectName, bool toogle)
        {
            foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                bool flag = gameObject.name.Contains(gameObjectName);
                if (flag)
                {
                    gameObject.active = toogle;
                }
            }
        }
    }
}
