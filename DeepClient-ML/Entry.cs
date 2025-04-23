using System;
using MelonLoader;
using DeepCore.Client;
using UnhollowerRuntimeLib;
using System.Linq;
using UnityEngine;
using System.Diagnostics;
using DeepCore.ServerAPI;

namespace DeepCore
{
    public class Entry : MelonMod
    {
        [Obsolete]
        public static bool IsBot = false;
        public static bool IsLoaded = false;
        public static bool IsInVR = false;
        public static string NumberBot = "0";
        public static string ProfileNumber = "0";

        [Obsolete]
        public override void OnInitializeMelon()
        {
            DeepConsole.Alloc();
            var args = Environment.GetCommandLineArgs().ToList();
            foreach (string text in args)
            {   
                if (text.Contains("DCDaddyUwU"))
                {
                    IsBot = true;
                    Application.targetFrameRate = 10;
                }
                else if (text.StartsWith("--Number="))
                {
                    NumberBot = text.Replace("--Number=", "");
                }
                else if (text.StartsWith("--profile="))
                {
                    ProfileNumber = text.Replace("--profile=", "").ToLower();
                }
            }
            try
            {
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            }
            catch (Exception ex)
            {}
            StartClient();
        }
        public static void StartClient()
        {
            try
            {
                ConfManager.initConfig();
                MelonPreferences.Load();
                DeepConsole.Art(IsBot);
                DeepConsole.Log("Startup", "Starting Client...");
                Client.Patching.Initpatches.Start();
                Injectories();
                QOLThings();
                Client.Coroutine.CoroutineManager.Init();
                Client.Misc.SpriteManager.LoadSprite();
                DeepConsole.Log("Startup", "Waiting for QM...");
                MelonCoroutines.Start(Client.UI.UIController.WaitForQM());
                IsLoaded = true;
            }
            catch (Exception ex)
            {
                DeepConsole.E(ex);
                Client.Misc.WMessageBox.HandleInternalFailure($"Client Startup failed: {ex.Message}",true);
            }
        }
        protected static void Injectories()
        {
            DeepConsole.Log("Startup", "Starting Injectories...");
            ClassInjector.RegisterTypeInIl2Cpp<Client.Mono.CustomNameplate>();
        }
        protected static void QOLThings()
        {
            DeepConsole.Log("Startup", "Starting QOLThings...");
            Client.Module.QOL.NoSteamAtAll.Start();
            Client.Module.QOL.CoreLimiter.Start();
            Client.Module.QOL.RamCleaner.StartMyCleaner();
            Client.Misc.Binds.Register();
            MelonCoroutines.Start(Client.Patching.GameVersionSpoofer.Init());
            if (ConfManager.BLSEnabled.Value)
            {
                Client.Module.QOL.OldLoadingScreenMod.OnApplicationStart();
            }
        }
        public override void OnUpdate()
        {
            if (IsLoaded)
            {
                Client.Module.Movement.UpdateModule.Update();
                Client.Module.Visual.UpdateModule.OnUpdate();
                Client.Module.QOL.KeyBindManager.OnUpdate();
                Client.UI.QM.QMPlayerList.UpdateList();
                Client.Module.Exploits.SwasticaOrbit.OnUpdate();
                Client.Module.QOL.ThirdPersonView.Update();
            }
        }
        public override void OnGUI()
        {
            Client.GUI.UpdateModule.UpdateGUI();
        }
        public override void OnSceneWasLoaded(int buildindex, string sceneName)
        {
            Client.Module.OnLoadedScaneManager.LoadedScene(buildindex, sceneName);
        }
        public override void OnApplicationQuit()
        {
            MelonPreferences.Save();
        }
    }
}
