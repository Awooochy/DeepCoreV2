using UnityEngine;
using VRC.Udon;

namespace DeepCore.Client.Module.WorldHacks
{
    internal class MagicFreezeTag
    {
        public static void StartGame()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Btn_Start");
        }
        public static void EndGame()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "NobodyVictory");
        }
        public static void RamdomTP()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "TeleportToRandom");
        }
        public static void RunnersWin()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "RunnerVictory");
        }
        public static void TaggersWin()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "TaggerVictory");
        }
    }
}
