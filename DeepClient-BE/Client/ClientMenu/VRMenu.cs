using System.Collections;
using UnityEngine;
using DeepClient.Client.Misc.DeepClient.Client.Misc;
using DeepClient.Client.API.ButtonAPI.QM;
using DeepClient.Client.Module.Exploits;
using DeepClient.Client.Module.Movement;
using DeepClient.Client.Module.Visuals;
using DeepClient.Client.Misc;
using VRC.SDKBase;

namespace DeepClient.Client.ClientMenu
{
    public class VRMenu : MonoBehaviour
    {
        void Start()
        {
            DeepConsole.Log("Startup", "Waiting for qm...");
            QMConsole.StartConsole().Start();
            if (ConfManager.ShouldMenuMusic.Value)
            {
                MenuMusic.MenuMusicInit().Start();
            }
            QMMenuDashPage().Start();
            QMMenuDevPage().Start();
            QMSelectedUser().Start();
        }
        private IEnumerator QMMenuDashPage()
        {
            while (GameObject.Find("Canvas_QuickMenu(Clone)") == null)
            {
                yield return null;
            }
            Module.Visuals.ThirdPersonView.OnStart();
            QMDashboard.Setup();
            QMDashboard.AddHeader("DeepClient", "DeepClient");
            QMDashboard.CreateButtonPref("DeepClient");
            QMDashboard.AddButton("DeepClient", "Flight", "Flight", "", () =>
            {
                Flight.FlyToggle();
            });
            QMDashboard.AddButton("DeepClient", "CapsuleESP", "Capsule ESP", "\"Allow you to see all players.", () =>
            {
                ESP.isCapEnabled = !ESP.isCapEnabled;
                ESP.CapsuleState(ESP.isCapEnabled);
            });
            QMDashboard.AddButton("DeepClient", "ObjectESP", "Object ESP", "Allow you to see all pickups.", () =>
            {
                ESP.isObjEnabled = !ESP.isObjEnabled;
                ESP.ObjectState(ESP.isObjEnabled);
            });
            QMDashboard.AddButton("DeepClient", "JoinByID", "Join By ID", "Allows you to join a world by it's ID.", () =>
            {
                Misc.PopupHelper.PopupCall("Join By ID", "Enter the ID to join", "Join", false, userInput =>
                {
                    DeepConsole.Log("IDJoiner", $"Joining ID: {userInput}");
                    Networking.GoToRoom(userInput);
                });
            });
        }
        private IEnumerator QMMenuDevPage()
        {
            while (GameObject.Find("Canvas_QuickMenu(Clone)") == null)
            {
                yield return null;
            }
            QMDevTools.Setup("DeepClient");
            QMDevTools.AddButton("LoudMic", "LoudMic", "Make your mic like CYKA BLYAT SHIT.", () =>
            {
                LoudMic.IsEnabled = !LoudMic.IsEnabled;
                LoudMic.State(LoudMic.IsEnabled);
            });
            QMDevTools.AddButton("E1", "E1", "We love E1 X3.", () =>
            {
                EarRape.Earape = !EarRape.Earape;
                EarRape.State(EarRape.Earape);
            });
            QMDevTools.AddButton("BigAvatar", "Big Avatar", "Make your avatar very big.", () =>
            {
                AvatarHeight.BigAvi = !AvatarHeight.BigAvi;
                AvatarHeight.BigState(AvatarHeight.BigAvi);
            });
            QMDevTools.AddButton("SmallAvata", "Small Avata", "Make your avatar very small.", () =>
            {
                AvatarHeight.SmallAvi = !AvatarHeight.SmallAvi;
                AvatarHeight.SmallState(AvatarHeight.SmallAvi);
            });
            QMDevTools.AddButton("ItemLagger", "Item Lagger", "This will make the lobby laggy.", () =>
            {
                ItemLagger.IsEnabled = !ItemLagger.IsEnabled;
                ItemLagger.State(ItemLagger.IsEnabled);
            });
            QMDevTools.AddButton("AntiTheft", "AntiTheft", "Prevent someones from stealing your object.", () =>
            {
                AntiTheft.IsEnabled = !AntiTheft.IsEnabled;
                AntiTheft.AntiPickupTheft(AntiTheft.IsEnabled);
            });
            QMDevTools.AddButton("UdonNuke", "Udon Nuke", "Nuke every udon in this world.", () =>
            {
                UdonNuker.Nuke().Start();
            });
            QMDevTools.AddButton("Unicode", "Unicode\nChat", "Send unicode message.", () =>
            {
                PhotonEx.SendChatBoxMessage("\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v\v");
            });
            QMDevTools.AddButton("DestroyPortals", "DestroyPortals", "Allow you to destroy all portals.", () =>
            {
                PortalEploits.DestroyPortals();
            });
        }
        private IEnumerator QMSelectedUser()
        {
            while (GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local") == null)
            {
                yield return null;
            }
            QMSelectedUserPage.AddHeader("DeepClient", "DeepClient");
            QMSelectedUserPage.CreateButtonPref("DeepClient");
            QMSelectedUserPage.AddButton("DeepClient", "DeepClient", "Teleport", true, () =>
            {

            });
        }
    }
}
