using DeepCore.Client.Misc;
using MelonLoader;
using UnityEngine;
using VRC.Core;

namespace DeepCore.Client.UI.QM
{
    internal class QMUI
    {
        public static void InitQM()
        {
            VrcExtensions.SetQmDashbordPageTittle($"{ClientUtils.GetGreeting()}!");
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/Header_H1/RightItemContainer").SetActive(false);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_Dashboard/ScrollRect/Viewport/VerticalLayoutGroup/Carousel_Banners").SetActive(false);
            GameObject.DestroyImmediate(GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_VRCPlus"));
            ClientMenu.MenuBuilder.MenuStart();
            Module.QOL.ThirdPersonView.OnStart();
            MelonCoroutines.Start(QMConsole.StartConsole());
            MelonCoroutines.Start(QMPlayerList.StartPlayerList());
            if (ConfManager.ShouldMenuMusic.Value)
            {
                MelonCoroutines.Start(MenuMusic.MenuMusicInit());
            }
            DeepConsole.Log("Startup", $"Welcome Back, {APIUser.CurrentUser.displayName}.");
            VrcExtensions.Toast("DeepCore V2",$"Welcome Back, {APIUser.CurrentUser.displayName}.");
            VrcExtensions.HudNotif("All systems online. (I hope so)");
        }
    }
}
