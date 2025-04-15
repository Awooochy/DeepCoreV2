using System.Collections;
using MelonLoader;
using ReMod.Core.UI.QuickMenu;
using UnityEngine;
using VRC.SDKBase;
using VRC.Udon;

namespace DeepCore.Client.Module.WorldHacks
{
    internal class JustBClub3
    {
        public static bool SpamSound;
        public static void JBC3Menu(ReMenuPage reCategoryPage)
        {
            ReCategoryPage reCategory = reCategoryPage.AddCategoryPage("JBC3");
            #region Room Unlocker
            reCategory.AddCategory("Room Unlocker");
            ReMenuCategory RoomUnlocker = reCategory.GetCategory("Room Unlocker");
            RoomUnlocker.AddButton("Unlock Summer", "Unlock Summer", delegate ()
            {
                MelonCoroutines.Start(UnlockRoom("Summer"));
            }, null, "#ffffff");
            RoomUnlocker.AddButton("Unlock Spring", "Unlock Spring", delegate ()
            {
                MelonCoroutines.Start(UnlockRoom("Spring"));
            }, null, "#ffffff");
            RoomUnlocker.AddButton("Unlock Winter", "Unlock Winter", delegate ()
            {
                MelonCoroutines.Start(UnlockRoom("Winter"));
            }, null, "#ffffff");
            RoomUnlocker.AddButton("Unlock Autumn", "Unlock Autumn", delegate ()
            {
                MelonCoroutines.Start(UnlockRoom("Autumn"));
            }, null, "#ffffff");
            RoomUnlocker.AddButton("Unlock VIPBed", "Unlock VIPBed", delegate ()
            {
                MelonCoroutines.Start(UnlockRoom("VIP Bedroom"));
            }, null, "#ffffff");
            #endregion
            #region RoomAnnoyer
            reCategory.AddCategory("Room Sound Annoyer");
            ReMenuCategory RoomAnnoyer = reCategory.GetCategory("Room Sound Annoyer");
            RoomAnnoyer.AddButton("Stop all spam", "Stop all spam.", delegate ()
            {
                SpamSound = false;
            }, null, "#ffffff");
            RoomAnnoyer.AddButton("Spam Summer", "Spam summer of sound.", delegate ()
            {
                SpamSound = true;
                MelonCoroutines.Start(SpamRoomSound("Summer"));
            }, null, "#ffffff");
            RoomAnnoyer.AddButton("Spam Spring", "Spam spring of sounds.", delegate ()
            {
                SpamSound = true;
                MelonCoroutines.Start(SpamRoomSound("Spring"));
            }, null, "#ffffff");
            RoomAnnoyer.AddButton("Spam Winter", "Spam winter of sounds.", delegate ()
            {
                SpamSound = true;
                MelonCoroutines.Start(SpamRoomSound("Winter"));
            }, null, "#ffffff");
            RoomAnnoyer.AddButton("Spam Autumn", "Spam autumn of sounds.", delegate ()
            {
                SpamSound = true;
                MelonCoroutines.Start(SpamRoomSound("Autumn"));
            }, null, "#ffffff");
            RoomAnnoyer.AddButton("Spam VIPBed", "Spam vipbed of sounds.", delegate ()
            {
                SpamSound = true;
                MelonCoroutines.Start(SpamRoomSound("VIP Bedroom"));
            }, null, "#ffffff");
            #endregion
            #region Room Telepoter
            reCategory.AddCategory("Room Telepoter");
            ReMenuCategory RoomTelepoter = reCategory.GetCategory("Room Telepoter");
            RoomTelepoter.AddButton("TP Room 1", "", TPTROOM1);
            RoomTelepoter.AddButton("TP viproom", "", TPVIPROOM);
            #endregion
        }
        #region TP VIPROOM
        public static void TPVIPROOM()
        {
            DeepConsole.Log("JBC3", "Trying to teleport LocalPlayer.");
            foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                bool flag2 = gameObject.name.Contains("VIP Lounge");
                if (flag2)
                {
                    gameObject.SetActive(true);
                }
            }
            Networking.LocalPlayer.gameObject.transform.position = new Vector3(-39.1f, 20f, 300f);
            DeepConsole.Log("JBC3", "LocalPlayer has been teleported -> VipRoom.");
        }
        #endregion
        #region TP ROOM 1
        public static void TPTROOM1()
        {
            DeepConsole.Log("JBC3", "Trying to teleport LocalPlayer.");
            foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                bool flag2 = gameObject.name.Contains("Bedroom");
                if (flag2)
                {
                    gameObject.SetActive(true);
                }
            }
            Networking.LocalPlayer.gameObject.transform.position = new Vector3(-29.1f, 19.8f, -200f);
            DeepConsole.Log("JBC3", "LocalPlayer has been teleported -> Room 1.");
        }
        #endregion
        #region UnlockRoom
        public static IEnumerator UnlockRoom(string room)
        {
            float startTime = Time.time;
            while (Time.time < startTime + 0.2f)
            {
                yield return null;
            }
            UdonBehaviour component = GameObject.Find(room).GetComponent<UdonBehaviour>();
            if (component.gameObject != null)
            {
                Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, component.gameObject);
                component.SendCustomEvent("_ResetRoom");
            }
            yield break;
        }
        #endregion
        #region SpamRoomSound
        public static IEnumerator SpamRoomSound(string room)
        {
            float startTime = Time.time;
            while (Time.time < startTime + 0.2f)
            {
                yield return null;
            }
            UdonBehaviour ObjectToSendTo = GameObject.Find(room).GetComponent<UdonBehaviour>();
            if (ObjectToSendTo.gameObject != null)
            {
                while (SpamSound)
                {
                    if (Networking.GetOwner(ObjectToSendTo.gameObject).displayName != Networking.LocalPlayer.displayName)
                    {
                        Networking.SetOwner(VRCPlayer.field_Internal_Static_VRCPlayer_0.field_Private_VRCPlayerApi_0, ObjectToSendTo.gameObject);
                    }
                    ObjectToSendTo.SendCustomNetworkEvent(0, "StartIntercom");
                    while (Time.time < startTime + 0.5f)
                    {
                        yield return null;
                    }
                    startTime = Time.time;
                }
            }
            yield break;
        }
        #endregion
    }
}
