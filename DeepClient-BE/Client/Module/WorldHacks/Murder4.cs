using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace DeepClient.Client.Module.WorldHacks
{
    internal class Murder4
    {
        public static void StartMatch()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Btn_Start");
            gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncStartGame");
        }
        public static void StopMatch()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAbort");
        }
        public static void BeARole(string PlayerName, string Role)
        {
            GameObject NodeObj = GameObject.Find("Game Logic/Player Nodes/Player Node (" + PlayerName + ")");
            NodeObj.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, Role);
        }
        public static void ReshowEveryoneRoles()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "OnLocalPlayerAssignedRole");
        }
        public static void BystandersWin()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVictoryB");
        }
        public static void MurderWin()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVictoryM");
        }
        public static void BringRevolver()
        {
            GameObject gameObject = GameObject.Find("Game Logic/Weapons/Revolver");
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            gameObject.transform.position = Networking.LocalPlayer.gameObject.transform.position + new Vector3(0f, 0.1f, 0f);
        }
        public static void BringShotgun()
        {
            GameObject gameObject = GameObject.Find("Game Logic/Weapons/Unlockables/Shotgun (0)");
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            gameObject.transform.position = Networking.LocalPlayer.gameObject.transform.position + new Vector3(0f, 0.1f, 0f);
        }
        public static void BringLuger()
        {
            GameObject gameObject = GameObject.Find("Game Logic/Weapons/Unlockables/Luger (0)");
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            gameObject.transform.position = Networking.LocalPlayer.gameObject.transform.position + new Vector3(0f, 0.1f, 0f);
        }
        public static void fireShotgun()
        {
            GameObject.Find("Game Logic/Weapons/Unlockables/Shotgun (0)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Fire");
        }
        public static void firerevolver()
        {
            GameObject.Find("Game Logic/Weapons/Revolver").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Fire");
        }
        public static void RevolverPatronSkin()
        {
            GameObject.Find("Game Logic/Weapons/Revolver").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "PatronSkin");
        }
        public static void fireLuger()
        {
            GameObject.Find("Game Logic/Weapons/Unlockables/Luger (0)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Fire");
        }
        public static void Frag0Explode()
        {
            GameObject gameObject = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
            gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Explode");
        }
        public static void ReleaseSnake()
        {
            GameObject.Find("Game Logic/Snakes/SnakeDispenser").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "DispenseSnake");
        }
        public static void CloseAllDoors()
        {
            List<Transform> list = new List<Transform>
            {
                GameObject.Find("Door").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (3)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (4)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (5)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (6)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (7)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (15)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (16)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (8)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (13)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (17)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (18)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (19)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (20)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (21)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (22)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (23)").transform.Find("Door Anim/Hinge/Interact close"),
                GameObject.Find("Door (14)").transform.Find("Door Anim/Hinge/Interact close")
            };
            foreach (Transform transform in list)
            {
                transform.GetComponent<UdonBehaviour>().Interact();
                LockAllDoors();
            }
        }
        public static void LockAllDoors()
        {
            List<Transform> list = new List<Transform>
            {
                GameObject.Find("Door").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (3)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (4)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (5)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (6)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (7)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (15)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (16)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (8)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (13)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (17)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (18)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (19)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (20)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (21)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (22)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (23)").transform.Find("Door Anim/Hinge/Interact lock"),
                GameObject.Find("Door (14)").transform.Find("Door Anim/Hinge/Interact lock")
            };
            foreach (Transform transform in list)
            {
                transform.GetComponent<UdonBehaviour>().Interact();
            }
        }
        public static void UnlockAllDoors()
        {
            List<Transform> list = new List<Transform>
            {
                GameObject.Find("Door").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (3)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (4)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (5)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (6)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (7)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (15)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (16)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (8)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (13)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (17)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (18)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (19)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (20)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (21)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (22)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (23)").transform.Find("Door Anim/Hinge/Interact shove"),
                GameObject.Find("Door (14)").transform.Find("Door Anim/Hinge/Interact shove")
            };
            foreach (Transform transform in list)
            {
                transform.GetComponent<UdonBehaviour>().Interact();
                transform.GetComponent<UdonBehaviour>().Interact();
                transform.GetComponent<UdonBehaviour>().Interact();
                transform.GetComponent<UdonBehaviour>().Interact();
            }
            OpenAllDoors();
        }
        public static void OpenAllDoors()
        {
            List<Transform> list = new List<Transform>
            {
                GameObject.Find("Door").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (3)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (4)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (5)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (6)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (7)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (15)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (16)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (8)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (13)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (17)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (18)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (19)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (20)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (21)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (22)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (23)").transform.Find("Door Anim/Hinge/Interact open"),
                GameObject.Find("Door (14)").transform.Find("Door Anim/Hinge/Interact open")
            };
            foreach (Transform transform in list)
            {
                transform.GetComponent<UdonBehaviour>().Interact();
            }
        }
        public static void FindHabibi()
        {
            Transform[] array = UnityEngine.Resources.FindObjectsOfTypeAll<Transform>();
            GameObject murdererName = null;
            int num;
            for (int j = 0; j < array.Length; j = num)
            {
                if (array[j].gameObject.name.Equals("Murderer Name"))
                {
                    murdererName = array[j].gameObject;
                }
                num = j + 1;
            }
            DeepConsole.Log("WorldHacks", $"{murdererName.GetComponent<Text>().text.ToString()}, Is the HABIBI");
        }
    }
}