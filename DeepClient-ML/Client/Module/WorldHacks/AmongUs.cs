using DeepCore.Client.Misc;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDK.Internal.MeetingBunker;
using VRC.SDKBase;
using VRC.Udon;

namespace DeepCore.Client.Module.WorldHacks
{
    internal class AmongUs
    {
        public static void AmongusMenu(ReMenuPage reCategoryPage)
        {
            ReCategoryPage reCategory = reCategoryPage.AddCategoryPage("Among Us");
            #region SelfEvent
            reCategory.AddCategory("Self Event");
            ReMenuCategory SelfEvent = reCategory.GetCategory("Self Event");
            SelfEvent.AddButton("Self\nImposter", "", delegate
            {
                SelfImposter(Networking.LocalPlayer.displayName);
            });
            SelfEvent.AddButton("Self\nCrewmate", "", delegate
            {
                SelfCrewmate(Networking.LocalPlayer.displayName);
            });
            #endregion
            #region Game Event
            reCategory.AddCategory("Game Event");
            ReMenuCategory GameEvent = reCategory.GetCategory("Game Event");
            GameEvent.AddButton("Start\nMatch\nCountdown", "",StartMatchCountdown);
            GameEvent.AddButton("Force\nStart\nMatch", "",ForceStartMatch);
            GameEvent.AddButton("Abort\nMatch", "",AbortMatch);
            GameEvent.AddButton("Kill\nAll", "",KillAll);
            GameEvent.AddButton("Start\nVote out\nEveryone", "",StartVoteOutEveryone);
            GameEvent.AddButton("Emergency\nButton", "",EmergencyButton);
            GameEvent.AddButton("All\nTasks\nDone", "",AllTasksDone);
            GameEvent.AddButton("Log\nImpostor", "",LogImpostor);
            GameEvent.AddButton("Crewmate\nWin", "",CrewmateWin);
            GameEvent.AddButton("Imposter\nWin", "",ImposterWin);
            GameEvent.AddButton("Everyone\nImposter", "",StartEveryoneInpostor);
            GameEvent.AddButton("Vote\nResult\nTie", "",SyncVoteResultTie);
            GameEvent.AddButton("Vote\nResult\nSkip", "", SyncVoteResultSkip);
            GameEvent.AddButton("End\nVoting\nPhase", "", SyncEndVotingPhase);
            #endregion
            #region Sabo Event
            reCategory.AddCategory("Sabo Event");
            ReMenuCategory SaboEvent = reCategory.GetCategory("Sabo Event");
            SaboEvent.AddButton("Stop All", "",StopSabbo);
            SaboEvent.AddButton("Repair\nComs", "",SyncRepairComms);
            SaboEvent.AddButton("Repair\nLights", "",SyncRepairLights);
            SaboEvent.AddButton("Repair\nOxygen", "",SyncRepairOxygenAB);
            SaboEvent.AddButton("Repair\nReactor", "",SyncRepairReactor);
            SaboEvent.AddButton("Sabo\nDoor", "",SaboDoor);
            SaboEvent.AddButton("Sabo\nLower\nDoor", "",SaboLowerDoor);
            SaboEvent.AddButton("Sabo\nUpper\nDoor", "",SaboUpperDoor);
            SaboEvent.AddButton("Sabo\nCafeteria\nDoor", "",SaboCafeteriaDoor);
            SaboEvent.AddButton("Sabo\nStorage\nDoor", "",SabStorageDoor);
            SaboEvent.AddButton("Sabo\nSecurity\nDoor", "",SabSecurityDoor);
            SaboEvent.AddButton("Sabo\nElectrical\nDoor", "",SabElectricalDoor);
            SaboEvent.AddButton("Sabo\nOxygen", "",SabOxygen);
            SaboEvent.AddButton("Sabo\nComs", "",SabComs);
            SaboEvent.AddButton("Sabo\nReactor", "",SabReactor);
            SaboEvent.AddButton("Sabo\nLights", "",SabLights);
            #endregion
        }
        public static void AmongTargetMenu(UiManager UIManager)
        {
            IButtonPage targetMenu = UIManager.TargetMenu;
            ReCategoryPage reMenu = targetMenu.AddCategoryPage("Among Us functions");
            #region UserRoles
            reMenu.AddCategory("User Roles");
            ReMenuCategory UserRole = reMenu.GetCategory("User Roles");
            UserRole.AddButton("Make crewmate", "", delegate ()
            {
                var Name = VrcExtensions.QM_GetSelectedUserName();
                SelfCrewmate(Name);
            });
            UserRole.AddButton("Make Imposter", "", delegate ()
            {
                var Name = VrcExtensions.QM_GetSelectedUserName();
                SelfImposter(Name);
            });
            #endregion
        }
        #region GameEvent
        #region Start Match Countdown
        public static void StartMatchCountdown()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            if (gameObject)
            {
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Btn_Start");
            }
        }
        #endregion
        #region Force Start Match
        public static void ForceStartMatch()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            if (gameObject)
            {
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "_start");
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncStart");
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncStartGame");
            }
        }
        #endregion
        #region Force Start MatchNML
        public static void ForceStartMatchNML()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            if (gameObject)
            {
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "_start");
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncStart");
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncStartGame");
            }
        }
        #endregion
        #region Abort Match
        public static void AbortMatch()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            if (gameObject)
            {
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAbort");
            }
        }
        #endregion
        #region KillAll
        public static void KillAll()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            if (gameObject)
            {
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "KillLocalPlayer");
            }
        }
        #endregion
        #region StartVoteOutEveryone
        public static void StartVoteOutEveryone()
        {
            for (int i = 0; i < 25; i++)
            {
                GameObject gameObject = GameObject.Find("Player Node (" + i.ToString() + ")");
                if (gameObject)
                {
                    gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVotedOut");
                }
            }
        }
        #endregion
        #region StartEveryoneInpostor
        public static void StartEveryoneInpostor()
        {
            for (int i = 0; i < 25; i++)
            {
                GameObject gameObject = GameObject.Find("Player Node (" + i.ToString() + ")");
                if (gameObject)
                {
                    gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAssignM");
                }
            }
        }
        #endregion
        #region EmergencyButton
        public static void EmergencyButton()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            if (gameObject)
            {
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "StartMeeting");
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncEmergencyMeeting");
            }
        }
        #endregion
        #region AllTasksDone
        public static void AllTasksDone()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            if (gameObject)
            {
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "OnLocalPlayerCompletedTask");
            }
        }
        #endregion
        #region LogImpostor
        public static void LogImpostor()
        {
            foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (gameObject.name.Contains("Player Entry") && gameObject.GetComponentInChildren<Text>().text != "PlayerName" && gameObject.GetComponent<Image>().color.r > 0f)
                {
                    VrcExtensions.HudNotif(gameObject.GetComponentInChildren<Text>().text + " is the imposter");
                }
            }
        }
        #endregion
        #region CrewmateWin
        public static void CrewmateWin()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVictoryC");
        }
        #endregion
        #region ImposterWin
        public static void ImposterWin()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVictoryI");
        }
        #endregion
        #region SaboDoor
        public static void SaboDoor()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageDoorsMedbay");
        }
        #endregion
        #region SaboLowerDoor
        public static void SaboLowerDoor()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageDoorsLower");
        }
        #endregion
        #region SaboUpperDoor
        public static void SaboUpperDoor()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageDoorsUpper");
        }
        #endregion
        #region SaboCafeteriaDoor
        public static void SaboCafeteriaDoor()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageDoorsCafeteria");
        }
        #endregion
        #region SabStorageDoor
        public static void SabStorageDoor()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageDoorsStorage");
        }
        #endregion
        #region SabSecurityDoor
        public static void SabSecurityDoor()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageDoorsSecurity");
        }
        #endregion
        #region SabElectricalDoor
        public static void SabElectricalDoor()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageDoorsElectrical");
        }
        #endregion
        #region SabOxygen
        public static void SabOxygen()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageOxygen");
        }
        #endregion
        #region SabComs
        public static void SabComs()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageComms");
        }
        #endregion
        #region SabReactor
        public static void SabReactor()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageReactor");
        }
        #endregion
        #region SabLights
        public static void SabLights()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncDoSabotageLights");
        }
        #endregion
        #region StopSabbo
        public static void StopSabbo()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "CancelAllSabotage");
        }
        #endregion
        #region SyncRepairComms
        public static void SyncRepairComms()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncRepairComms");
        }
        #endregion
        #region SyncRepairLights
        public static void SyncRepairLights()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncRepairLights");
        }
        #endregion
        #region SyncRepairOxygenAB
        public static void SyncRepairOxygenAB()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncRepairOxygenA");
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncRepairOxygenB");
        }
        #endregion
        #region SyncRepairReactor
        public static void SyncRepairReactor()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncRepairReactor");
        }
        #endregion
        #region SyncVoteResultTie
        public static void SyncVoteResultTie()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVoteResultTie");
        }
        #endregion
        #region SyncVoteResultSkip
        public static void SyncVoteResultSkip()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVoteResultSkip");
        }
        #endregion
        #region SyncVoteResultNobody
        public static void SyncVoteResultNobody()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVoteResultNobody");
        }
        #endregion
        #region SyncEndVotingPhase
        public static void SyncEndVotingPhase()
        {
            GameObject.Find("Game Logic").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncEndVotingPhase");
        }
        #endregion
        #endregion
        #region Self Stuff
        #region SelfImposter
        public static void SelfImposter(string username)
        {
            string value = username;
            for (int i = 0; i < 24; i++)
            {
                string text = "Player Node (" + i.ToString() + ")";
                if (GameObject.Find("Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text").GetComponent<Text>().text.Equals(value))
                {
                    GameObject.Find(text).GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAssignM");
                }
            }
        }
        #endregion
        #region SelfCrewmate
        public static void SelfCrewmate(string username)
        {
            string value = username;
            for (int i = 0; i < 24; i++)
            {
                string text = "Player Node (" + i.ToString() + ")";
                if (GameObject.Find("Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text").GetComponent<Text>().text.Equals(value))
                {
                    GameObject.Find(text).GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAssignB");
                }
            }
        }
        #endregion
        #endregion
    }
}
