using System;
using System.Reflection;
using DeepClient.Client.Patching.Modules;
using HarmonyLib;

namespace DeepClient.Client.Patching
{
    internal class InitPatch
    {
        public static string ModuleName = "HookManager";
        public static int pass = 0;
        public static int fail = 0;
        public static void Start()
        {
            DeepConsole.Log("Startup", "Starting Hooks...");
            try
            {
                ForceClone.Patch(); pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "allowAvatarCopying:" + ex.Message); fail++;
            }
            try
            {
                Spoofer.InitSpoofs(); pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "Spoofer:" + ex.Message); fail++;
            }
            try
            {
                EasyPatching.DeepCoreInstance.PatchAll(typeof(HighlightColor)); pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "HighlightColor:" + ex.Message); fail++;
            }
            try
            {
                LoadBalancingClienta.Patch(); pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "LoadBalancingClient.OnEvent:" + ex.Message); fail++;
            }
            try
            {
                OnAvatarChanged.Patch(); pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "OnAvatarChanged:" + ex.Message); fail++;
            }
            try
            {
                RoomManagera.Patch(); pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "RoomManager:" + ex.Message); fail++;
            }
            try
            {
                TriggerWorld.Patch(); pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "TriggerWorld:" + ex.Message); fail++;
            }
            try
            {
                UdonSync.Patch(); pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "UdonSync:" + ex.Message); fail++;
            }
            try
            {
                EasyPatching.DeepCoreInstance.Patch(typeof(QuickMenu).GetMethod("OnEnable"), GetLocalPatch("QmOpen"), null, null, null, null); pass++;
                EasyPatching.DeepCoreInstance.Patch(typeof(QuickMenu).GetMethod("OnDisable"), GetLocalPatch("QmClose"), null, null, null, null); pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "QuickMenu:" + ex.Message); fail++;
            }
            try
            {
                //NetworkManagerPatch.Patch(); pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "NetworkManager:" + ex.Message); fail++;
            }
            DeepConsole.Log(ModuleName, $"Placed {pass} hook successfully, with {fail} failed.");
        }
        private static void QmOpen()
        {
            Coroutines.CustomMenuBG.ApplyColors();
            Module.Movement.QMFreeze.State(true);
            ClientMenu.MenuMusic.State(true);
        }
        private static void QmClose()
        {
            ClientMenu.MenuMusic.State(false);
            Module.Movement.QMFreeze.State(false);
        }
        public static HarmonyMethod GetLocalPatch(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                DeepConsole.Log(ModuleName, "Method name cannot be null or empty.");
                return null;
            }
            try
            {
                MethodInfo method = typeof(InitPatch).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic);
                if (method == null)
                {
                    DeepConsole.Log(ModuleName, $"Method '{name}' not found in InitPatch.");
                    return null;
                }
                return ToHarmonyMethod(method);
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, $"Error in GetLocalPatch '{name}': {ex}");
                return null;
            }
        }
        private static HarmonyMethod ToHarmonyMethod(MethodInfo method)
        {
            return method != null ? new HarmonyMethod(method) : null;
        }
    }
}
