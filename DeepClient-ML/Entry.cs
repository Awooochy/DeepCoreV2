using System;
using MelonLoader;
using DeepCore.Client;
using UnhollowerRuntimeLib;
using System.Linq;
using UnityEngine;
using System.Diagnostics;
using DeepCore.ServerAPI;
using DeepCore.Client.Misc;
using DeepCore.Client.GUI;

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
            
            //Lets make it Priority High for it to work better.
            try
            {
                Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
            }
            catch (Exception ex)
            {}
            //here we start the client

            AuthAPI API = new AuthAPI();
            API.Auth();
            StartClient();
        }

        public static void StartClient()
        {
            try
            {
                ConfManager.initConfig();
                MelonPreferences.Load();
                DeepConsole.Art();
                DeepConsole.Log("Startup", "Starting Client...");
                
                // Initialize all systems
                Client.Patching.Initpatches.Start();
                Injectories();
                QOLThings();
                Client.Coroutine.CoroutineManager.Init();
                
                // Updated sprite loading call
                SpriteManager.LoadAllSprites(); // Changed from LoadSprite() to LoadAllSprites()
                
                DeepConsole.Log("Startup", "Waiting for QM...");
                MelonCoroutines.Start(Client.UI.UIController.WaitForQM());
                IsLoaded = true;
            }
            catch (Exception ex)
            {
                DeepConsole.LogException(ex);
                Client.Misc.WMessageBox.HandleInternalFailure($"Client Startup failed: {ex.Message}", true);
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