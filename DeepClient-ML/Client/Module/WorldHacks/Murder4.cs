using System.Collections;
using System.Collections.Generic;
using DeepCore.Client.Misc;
using MelonLoader;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;
using VRC.Udon;

namespace DeepCore.Client.Module.WorldHacks
{
    internal class Murder4
    {
        public static bool knifeShieldbool = false;
        public static void Murder4Menu(ReMenuPage reCategoryPage)
        {
            ReCategoryPage reCategory = reCategoryPage.AddCategoryPage("Murder 4");
            #region SelfEvent
            reCategory.AddCategory("Self Event");
            ReMenuCategory SelfEvent = reCategory.GetCategory("Self Event");
            SelfEvent.AddButton("Become murder", "", delegate ()
            {
                BeARole(Networking.LocalPlayer.displayName, "SyncAssignM");
            });
            SelfEvent.AddButton("Become bystender", "", delegate ()
            {
                BeARole(Networking.LocalPlayer.displayName, "SyncAssignB");
            });
            SelfEvent.AddButton("Become detective", "", delegate ()
            {
                BeARole(Networking.LocalPlayer.displayName, "SyncAssignD");
            });
            #endregion
            #region Game Event
            reCategory.AddCategory("Game Event");
            ReMenuCategory GameEvent = reCategory.GetCategory("Game Event");
            GameEvent.AddButton("Start\nMatch", "", StartMatch);
            GameEvent.AddButton("Abort\nMatch", "", AbortMatch);
            GameEvent.AddButton("Show\nRoles", "", ReshowEveryoneRoles);
            GameEvent.AddButton("Bystander\nWin", "", BystandersWin);
            GameEvent.AddButton("Murder\nWin", "", MurderWin);
            GameEvent.AddButton("Blind\nAll", "", BlindAll);
            GameEvent.AddButton("Kill\nAll", "", KillAll);
            GameEvent.AddButton("Close & Lock\ndoors", "", CloseAllDoors);
            GameEvent.AddButton("Open & Unlock\ndoors", "", OpenAllDoors);
            GameEvent.AddButton("Camera\nFlash", "", CameraFlash);
            #endregion
            #region GunEvent
            reCategory.AddCategory("Guns Events");
            ReMenuCategory GunEvent = reCategory.GetCategory("Guns Events");
            GunEvent.AddButton("Bring\nRevolver", "", BringRevolver);
            GunEvent.AddButton("Bring\nShotgun", "", BringShotgun);
            GunEvent.AddButton("Bring\nLuger", "", BringLuger);
            GunEvent.AddSpacer();
            GunEvent.AddButton("Fire\nRevolver", "", firerevolver);
            GunEvent.AddButton("Fire\nShotgun", "", fireShotgun);
            GunEvent.AddButton("Fire\nLuger", "", fireLuger);
            #endregion
            #region Other Events
            reCategory.AddCategory("Other Events");
            ReMenuCategory OtherEvent = reCategory.GetCategory("Other Events");
            OtherEvent.AddButton("Revolver\nPatron\nSkin", "", RevolverPatronSkin);
            OtherEvent.AddButton("Release\nSnake", "", ReleaseSnake);
            OtherEvent.AddButton("Bring\nSmoke\nGrenade", "", BringSmokeGrenade);
            OtherEvent.AddButton("Bring\nFrag", "", delegate ()
            {
                BringFrag(VRCPlayer.field_Internal_Static_VRCPlayer_0, false);
            });
            OtherEvent.AddButton("Explode\nFrag0", "", Frag0Explode);
            OtherEvent.AddButton("Explode\nTraps", "", BringTraps);
            OtherEvent.AddButton("Explode\nFlashCamera", "", BringFlashCamera);
            #endregion
        }
        public static void Murder4TargetMenu(UiManager UIManager)
        {
            IButtonPage targetMenu = UIManager.TargetMenu;
            ReCategoryPage reMenu = targetMenu.AddCategoryPage("Murder 4 functions");
            #region UserRoles
            reMenu.AddCategory("User Roles");
            ReMenuCategory UserRole = reMenu.GetCategory("User Roles");
            UserRole.AddButton("Make murder", "", delegate ()
            {
                var Name = VrcExtensions.QM_GetSelectedUserName();
                BeARole(Name, "SyncAssignM");
            });
            UserRole.AddButton("Make bystender", "", delegate ()
            {
                var Name = VrcExtensions.QM_GetSelectedUserName();
                BeARole(Name, "SyncAssignB");
            });
            UserRole.AddButton("Make detective", "", delegate ()
            {
                var Name = VrcExtensions.QM_GetSelectedUserName();
                BeARole(Name, "SyncAssignD");
            });
            UserRole.AddButton("Kill", "", delegate ()
            {
                var Name = VrcExtensions.QM_GetSelectedUserName();
                BeARole(Name, "yncKill");
            });
            #endregion
            #region Other things
            reMenu.AddCategory("Other things");
            ReMenuCategory Otherthings = reMenu.GetCategory("Other things");
            Otherthings.AddButton("Blow up player", "", delegate ()
            {
                BringFrag(ReMod.Core.VRChat.PlayerExtensions.GetVRCPlayer(), true);
            });
            Otherthings.AddToggle("Knife Shield", "Sigma simga boy.", delegate (bool s)
            {
                if (s)
                {
                    knifeShieldbool = true;
                    MelonCoroutines.Start(KnifeShieldCoroutine(ReMod.Core.VRChat.PlayerExtensions.GetVRCPlayer()));
                }
                else
                {
                    knifeShieldbool = false;
                }
            });
            #endregion
        }
        #region GameEvent etc
        #region Game Event
        #region StartMatch
        public static void StartMatch()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Btn_Start");
            gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncStartGame");
        }
        #endregion
        #region AbortMatch
        public static void AbortMatch()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAbort");
        }
        #endregion
        #region ReshowEveryoneRoles
        public static void ReshowEveryoneRoles()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "OnLocalPlayerAssignedRole");
        }
        #endregion
        #region BystandersWin
        public static void BystandersWin()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVictoryB");
        }
        #endregion
        #region MurderWin
        public static void MurderWin()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncVictoryM");
        }
        #endregion
        #region CloseAllDoors
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
            }
            LockAllDoors();
        }
        #endregion
        #region LockAllDoors
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
        #endregion
        #region UnlockAllDoors
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
        #endregion
        #region OpenAllDoors
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
        #region BlindAll
        public static void BlindAll()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            if (gameObject)
            {
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "OnLocalPlayerBlinded");
            }
        }
        #endregion
        #region CameraFlash
        public static void CameraFlash()
        {
            GameObject gameObject = GameObject.Find("Game Logic");
            if (gameObject)
            {
                gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "OnLocalPlayerFlashbanged");
            }
        }
        #endregion
        #endregion
        #region Brings things
        #region BringRevolver
        public static void BringRevolver()
        {
            GameObject gameObject = GameObject.Find("Game Logic/Weapons/Revolver");
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            gameObject.transform.position = Networking.LocalPlayer.gameObject.transform.position + new Vector3(0f, 0.1f, 0f);
        }
        #endregion
        #region BringShotgun
        public static void BringShotgun()
        {
            GameObject gameObject = GameObject.Find("Game Logic/Weapons/Unlockables/Shotgun (0)");
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            gameObject.transform.position = Networking.LocalPlayer.gameObject.transform.position + new Vector3(0f, 0.1f, 0f);
        }
        #endregion
        #region BringLuger
        public static void BringLuger()
        {
            GameObject gameObject = GameObject.Find("Game Logic/Weapons/Unlockables/Luger (0)");
            Networking.SetOwner(Networking.LocalPlayer, gameObject);
            gameObject.transform.position = Networking.LocalPlayer.gameObject.transform.position + new Vector3(0f, 0.1f, 0f);
        }
        #endregion
        #region BringSmokeGrenade
        public static void BringSmokeGrenade()
        {
            GameObject gameObject = GameObject.Find("Game Logic/Weapons/Unlockables/Smoke (0)");
            if (gameObject)
            {
                Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject);
                gameObject.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
            }
        }
        #endregion
        #region BringFrag Arg (VRCPlayer,bool)
        public static void BringFrag(VRCPlayer player,bool shouldblow)
        {
            GameObject gameObject = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
            if (gameObject)
            {
                Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject);
                gameObject.transform.position = player.transform.position + new Vector3(0f, 0.1f, 0f);
            }
            if (shouldblow)
            {
                Frag0Explode();
                DeepConsole.Log("M4", $"Imagine {player.field_Private_VRCPlayerApi_0.displayName} blowed up. XD");
            }
        }
        #endregion
        #region BringTraps
        public static void BringTraps()
        {
            GameObject gameObject = GameObject.Find("Game Logic/Weapons/Bear Trap (0)");
            if (gameObject)
            {
                Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject);
                gameObject.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
            }
            GameObject gameObject2 = GameObject.Find("Game Logic/Weapons/Bear Trap (1)");
            if (gameObject2)
            {
                Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject2);
                gameObject2.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
            }
            GameObject gameObject3 = GameObject.Find("Game Logic/Weapons/Bear Trap (2)");
            if (gameObject3)
            {
                Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject3);
                gameObject3.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
            }
        }
        #endregion
        #region BringFlashCamera
        public static void BringFlashCamera()
        {
            GameObject gameObject = GameObject.Find("Game Logic/Polaroids Unlock Camera/FlashCamera");
            if (gameObject)
            {
                Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, gameObject);
                gameObject.transform.position = VRCPlayer.field_Internal_Static_VRCPlayer_0.transform.position + new Vector3(0f, 0.1f, 0f);
            }
        }
        #endregion
        #region KnifeShieldCoroutine
        public static IEnumerator KnifeShieldCoroutine(VRCPlayer player)
        {
            List<GameObject> list = new List<GameObject>
            {
                GameObject.Find("Game Logic").transform.Find("Weapons/Knife (0)").gameObject,
                GameObject.Find("Game Logic").transform.Find("Weapons/Knife (1)").gameObject,
                GameObject.Find("Game Logic").transform.Find("Weapons/Knife (2)").gameObject,
                GameObject.Find("Game Logic").transform.Find("Weapons/Knife (3)").gameObject,
                GameObject.Find("Game Logic").transform.Find("Weapons/Knife (4)").gameObject,
                GameObject.Find("Game Logic").transform.Find("Weapons/Knife (5)").gameObject
            };
            GameObject gameObject = new GameObject();
            gameObject.transform.position = player.transform.position + new Vector3(0f, 0.35f, 0f);
            while (knifeShieldbool)
            {
                if (!knifeShieldbool)
                {
                    break;
                }
                gameObject.transform.Rotate(new Vector3(0f, 360f * Time.time, 0f));
                gameObject.transform.position = player.transform.position + new Vector3(0f, 0.35f, 0f);
                foreach (GameObject gameObject2 in list)
                {
                    Networking.LocalPlayer.TakeOwnership(gameObject2.gameObject);
                    gameObject2.transform.position = gameObject.transform.position + gameObject.transform.forward;
                    gameObject2.transform.LookAt(player.transform);
                    gameObject.transform.Rotate(new Vector3(0f, (float)(360 / list.Count), 0f));
                }
                yield return null;
            }
            Object.Destroy(gameObject);
            gameObject = null;
        }
        #endregion
        #endregion
        #region Fire things
        #region fireShotgun
        public static void fireShotgun()
        {
            GameObject.Find("Game Logic/Weapons/Unlockables/Shotgun (0)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Fire");
        }
        #endregion
        #region firerevolver
        public static void firerevolver()
        {
            GameObject.Find("Game Logic/Weapons/Revolver").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Fire");
        }
        #endregion
        #region fireLuger
        public static void fireLuger()
        {
            GameObject.Find("Game Logic/Weapons/Unlockables/Luger (0)").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Fire");
        }
        #endregion
        #region Frag0Explode
        public static void Frag0Explode()
        {
            GameObject gameObject = GameObject.Find("Game Logic/Weapons/Unlockables/Frag (0)");
            gameObject.GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "Explode");
        }
        #endregion
        #endregion
        #region RevolverPatronSkin
        public static void RevolverPatronSkin()
        {
            GameObject.Find("Game Logic/Weapons/Revolver").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "PatronSkin");
        }
        #endregion
        #region ReleaseSnake
        public static void ReleaseSnake()
        {
            GameObject.Find("Game Logic/Snakes/SnakeDispenser").GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "DispenseSnake");
        }
        #endregion
        #region FindMurder
        public static void FindMurder()
        {
            Transform[] array = Resources.FindObjectsOfTypeAll<Transform>();
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
            DeepConsole.Log("M4", $"{murdererName.GetComponent<TextMeshProUGUI>().text.ToString()}, Is the murder.");
            VrcExtensions.HudNotif($"{murdererName.GetComponent<TextMeshProUGUI>().text.ToString()}, Is the murder.");
        }
        #endregion
        #region BeARole
        public static void BeARole(string username, string role)
        {
            string value = username;
            for (int i = 0; i < 24; i++)
            {
                string text = "Player Node (" + i.ToString() + ")";
                if (GameObject.Find("Game Logic/Game Canvas/Game In Progress/Player List/Player List Group/Player Entry (" + i.ToString() + ")/Player Name Text").GetComponent<TextMeshProUGUI>().text.Equals(value))
                {
                    MelonLogger.Msg(text);
                    GameObject.Find(text).GetComponent<UdonBehaviour>().SendCustomNetworkEvent(0, "SyncAssignM");
                }
            }
        }
        #endregion
        #endregion
        public static IEnumerator InitTheme()
        {
            while (GameObject.Find("Game Logic/Game Canvas") == null)
            {
                yield return null;
            }
            GameObject.Find("Game Logic/Game Canvas/Pregame/Title Text").GetComponent<TextMeshProUGUI>().text = "HABIBI 4";
            GameObject.Find("Game Logic/Game Canvas/Pregame/Title Text").GetComponent<TextMeshProUGUI>().color = Color.red;
            GameObject.Find("Game Logic/Game Canvas/Pregame/Author Text").GetComponent<TextMeshProUGUI>().text = "By Osama";
            GameObject.Find("Game Logic/Game Canvas/Pregame/Author Text").GetComponent<TextMeshProUGUI>().color = Color.red;
            GameObject.Find("Game Logic/Game Canvas/Background Panel Border").GetComponent<Image>().color = Color.red;
            DeepConsole.LogConsole("M4", "Disabling useless hud...");
            GameObject.Find("Game Logic/Player HUD/Death HUD Anim").SetActive(false);
            GameObject.Find("Game Logic/Player HUD/Blind HUD Anim").SetActive(false);
            yield break;
        }
    }
}