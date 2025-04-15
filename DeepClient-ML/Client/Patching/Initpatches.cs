using System;
using System.Reflection;
using ExitGames.Client.Photon;
using HarmonyLib;
using MelonLoader;
using Photon.Realtime;
using VRC.Economy;
using VRC.SDKBase;

namespace DeepCore.Client.Patching
{
    internal class Initpatches
    {
        public static string ModuleName = "HookManager";
        public static readonly HarmonyLib.Harmony instance = new HarmonyLib.Harmony("DeepClient.ultrapatch");
        public static int pass = 0;
        public static int fail = 0;
        private static HarmonyMethod GetPreFix(string methodName)
        {
            return new HarmonyMethod(typeof(Initpatches).GetMethod(methodName, BindingFlags.Static | BindingFlags.NonPublic));
        }
        [Obsolete]
        public static void Start()
        {
            DeepConsole.Log("Startup", "Starting Hooks...");
            try
            {
                ClonePatch.Patch();
                pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "allowAvatarCopying:" + ex.Message);
                fail++;
            }
            try
            {
                SpooferPatch.InitSpoofs();
                pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "Spoofer:" + ex.Message);
                fail++;
            }
            try
            {
                EasyPatching.DeepCoreInstance.PatchAll(typeof(HighlightColorPatch));
                pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "HighlightColor:" + ex.Message);
                fail++;
            }
            try
            {
                LoadBalancingClientPatch.Patch();
                pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "LoadBalancingClient.OnEvent:" + ex.Message);
                fail++;
            }
            try
            {
                EasyPatching.DeepCoreInstance.Patch(typeof(LoadBalancingClient).GetMethod("Method_Public_Virtual_New_Boolean_Byte_Object_RaiseEventOptions_SendOptions_0"), GetLocalPatch("Patch_OnEventSent"), null, null, null, null);
                pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "LoadBalancingClient.OnEventSent:" + ex.Message);
                fail++;
            }
            try
            {
                instance.Patch(typeof(VRC_EventDispatcherRFC).GetMethod("Method_Public_Boolean_Player_VrcEvent_VrcBroadcastType_0"), new HarmonyMethod(typeof(Initpatches).GetMethod("RPCPatch", BindingFlags.Static | BindingFlags.NonPublic)), null, null, null, null);
                DeepConsole.LogConsole("Hook", "Look daddy, I can see rpc now.");
                pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "VRC_EventDispatcherRFC.RPC:" + ex.Message);
                fail++;
            }
            try
            {
                OnAvatarChangedPatch.Patch();
                pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName,"OnAvatarChanged:"+ex.Message);
                fail++;
            }
            try
            {
                RoomManagerPatch.Patch();
                pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "RoomManager:" + ex.Message);
                fail++;
            }
            try
            {
                UdonSyncPatch.Patch();
                pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "UdonSync:" + ex.Message);
                fail++;
            }
            try
            {
                EasyPatching.DeepCoreInstance.Patch(typeof(VRCPlusStatus).GetProperty("prop_Object1PublicTYBoTYUnique_1_Boolean_0").GetGetMethod(), null,GetLocalPatch("GetVRCPlusStatus"), null, null, null);
                pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "VRCPlusStatus:" + ex.Message);
                fail++;
            }
            try
            {
                instance.Patch(typeof(Store).GetMethod("Method_Private_Boolean_VRCPlayerApi_IProduct_PDM_0"), Initpatches.GetPreFix("RetrunPrefix"), null, null, null, null);
                instance.Patch(typeof(Store).GetMethod("Method_Private_Boolean_IProduct_PDM_0"), Initpatches.GetPreFix("RetrunPrefix"), null, null, null, null);
                pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "Store:" + ex.Message);
                fail++;
            }
            try
            {
                //EasyPatching.DeepCoreInstance.Patch(typeof(QuickMenu).GetMethod("OnEnable"),GetLocalPatch("QmOpen"), null, null, null, null);
                //EasyPatching.DeepCoreInstance.Patch(typeof(QuickMenu).GetMethod("OnDisable"),GetLocalPatch("QmClose"), null, null, null, null);
                //EasyPatching.DeepCoreInstance.Patch(typeof(MainMenu).GetMethod("OnEnable"),GetLocalPatch("QmOpen"), null, null, null, null);
                //EasyPatching.DeepCoreInstance.Patch(typeof(MainMenu).GetMethod("OnDisable"),GetLocalPatch("QmClose"), null, null, null, null);
                pass++;
            }
            catch (Exception ex)
            {
                DeepConsole.Log(ModuleName, "QuickMenu:" + ex.Message);
                fail++;
            }
            DeepConsole.Log(ModuleName, $"Placed {pass} hook successfully, with {fail} failed.");
        }
        private static bool RPCPatch(VRC.Player param_1, VRC_EventHandler.VrcEvent param_2, VRC_EventHandler.VrcBroadcastType param_3)
        {
            try
            {
                Module.RPC.RPCManager.HandleRPC(param_1,param_2,param_3); 
                return true;
            }
            catch (Exception ex)
            {
                MelonLogger.Msg(ex.ToString());
            }
            return true;
        }
        #region MenuShit
        private static void QmOpen()
        {
            if (ConfManager.ShouldQMFreeze.Value)
            {
                Module.QOL.QMFreeze.State(true);
            }
            Coroutine.CustomMenuBG.ApplyColors();
            //UI.QM.MenuMusic.State(true);
        }
        private static void QmClose()
        {
            if (ConfManager.ShouldQMFreeze.Value)
            {
                Module.QOL.QMFreeze.State(false);
            }
            //UI.QM.MenuMusic.State(false);
        }
        #endregion
        private static void GetVRCPlusStatus(ref Object1PublicTYBoTYUnique<bool> __result)
        {
            bool flag = __result == null;
            if (!flag)
            {
                __result.prop_TYPE_0 = true;
            }
        }
        #region StoreShit
        private static bool MarketPatch(VRCPlayerApi __0, IProduct __1, ref bool __result)
        {
            __result = true;
            return false;
        }
        private static bool RetrunPrefix(ref bool __result)
        {
            __result = true;
            return false;
        }
        #endregion
        internal static bool Patch_OnEventSent(byte __0, object __1, RaiseEventOptions __2, SendOptions __3)
        {
            if (Module.Photon.MovementSerilize.IsEnabled)
            {
                return Module.Photon.MovementSerilize.OnEventSent(__0, __1, __2, __3);
            }
            if (Module.Photon.PhotonDebugger.IsOnEventSendDebug)
            {
                return Module.Photon.PhotonDebugger.OnEventSent(__0, __1, __2, __3);
            }
            return true;
        }

        public static HarmonyMethod GetLocalPatch(string name)
        {
            HarmonyMethod result;
            try
            {
                result = (HarmonyMethod)typeof(Initpatches).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic).ToNewHarmonyMethod();
            }
            catch (Exception arg)
            {
                DeepConsole.Log(ModuleName, (string.Format("{0}: {1}", name, arg)));
                result = null;
            }
            return result;
        }
    }
}
