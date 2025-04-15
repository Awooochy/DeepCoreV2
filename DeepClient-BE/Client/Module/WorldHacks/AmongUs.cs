using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;
using UnityEngine.UI;

namespace DeepClient.Client.Module.WorldHacks
{
    internal class AmongUs
    {
        #region Game Event
        #region Start Match Countdown
        public static void StartMatchCountdown()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            {
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Btn_Start");
                DeepConsole.Log("AU", "Forced Match Countdown.");
            }
        }
        #endregion
        #region Force Start Match
        public static void ForceStartMatch()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            {
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "_start");
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncStart");
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncStartGame");
            }
            DeepConsole.Log("AU", "Forced start match.");
        }
        #endregion
        #region KillAll
        public static void KillAll()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            {
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "KillLocalPlayer");
            }
            DeepConsole.Log("AU", "Killed everyones.");
        }
        #endregion
        #region VoteOutEveryone
        public static void StartVoteOutEveryone()
        {
            for (int i = 0; i < 25; i++)
            {
                GameObject gameObject = GameObject.Find("Player Node (" + i.ToString() + ")");
                {
                    gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVotedOut");
                }
                DeepConsole.Log("AU", "Forced vote out everyone.");
            }
        }
        #endregion
        #region EmergencyButton
        public static void EmergencyButton()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            {
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "StartMeeting");
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncEmergencyMeeting");
            }
            DeepConsole.Log("AU", "Forced emergency meeting.");
        }
        #endregion
        #region All Tasks Done
        public static void AllTasksDone()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "OnLocalPlayerCompletedTask");
            DeepConsole.Log("AU", "Forced all tasks.");
        }
        #endregion
        #region Inposter win
        public static void InposterWin()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVictoryI");
        }
        #endregion
        #region Crewmates win
        public static void CrewmatesWin()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVictoryC");
        }
        #endregion
        #region Skip vote
        public static void skipvote()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Btn_SkipVoting");
        }
        #endregion
        #region LightOff
        public static void LightOff()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageLights");
        }
        #endregion
        #region Reactor
        public static void ReactorSab()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageReactor");
        }
        #endregion
        #region Oxygen
        public static void OxygenSab()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageOxygen");
        }
        #endregion
        #endregion
        #region Self Stuff
        #region SelfImposter
        public static void SelfImposter()
        {
            string value = Networking.LocalPlayer.displayName;
            for (int i = 0; i < 24; i++)
            {
                string text = "Player Node (" + i.ToString() + ")";
                if (GameObject.Find("Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text").GetComponent<Text>().text.Equals(value))
                {
                    GameObject.Find(text).GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAssignM");
                }
                DeepConsole.Log("AU", "Forced self imposter !");
            }
        }
        #endregion
        #region SelfCrewmate
        public static void SelfCrewmate()
        {
            string value = Networking.LocalPlayer.displayName;
            for (int i = 0; i < 24; i++)
            {
                string text = "Player Node (" + i.ToString() + ")";
                if (GameObject.Find("Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text").GetComponent<Text>().text.Equals(value))
                {
                    GameObject.Find(text).GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAssignB");
                }
                DeepConsole.Log("AU", "Forced self crewmate !");
            }
        }
        #endregion
        #region MakePersonAInposter
        public static void MakePersonAInposter(string Name, string Role)
        {
            string value = Name;
            for (int i = 0; i < 24; i++)
            {
                string text = "Player Node (" + i.ToString() + ")";
                if (GameObject.Find("Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text").GetComponent<Text>().text.Equals(value))
                {
                    GameObject.Find(text).GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, Role);
                }
                DeepConsole.Log("AU", $"Maked {Name} Crewmate/Inposter !");
            }
        }
        #endregion
        #endregion
    }
}
