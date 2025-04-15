using System.Collections;
using System.Linq;
using DeepCore.Client.Misc;
using MelonLoader;
using UnhollowerBaseLib;
using UnityEngine;
using VRC;
using VRC.Networking;
using VRC.SDK.Internal;
using VRC.SDK3.Components;
using VRC.Udon;
using VRCSDK2;

namespace DeepCore.Client.Module
{
    internal class OnLoadedScaneManager
    {
        internal static float oldRespawnHight;
        internal static Il2CppArrayBase<global::VRCSDK2.VRC_Pickup> sdk2Items;
        internal static Il2CppArrayBase<VRCPickup> sdk3Items;
        internal static VRC_ObjectSync[] allSyncItems;
        internal static Il2CppArrayBase<VRCObjectSync> allSDK3SyncItems;
        internal static Il2CppArrayBase<VRCObjectPool> allPoolItems;
        internal static global::VRC.SDKBase.VRC_Pickup[] allBaseUdonItem;
        internal static Il2CppArrayBase<VRCInteractable> allInteractable;
        internal static Il2CppArrayBase<global::VRC.SDKBase.VRC_Interactable> allBaseInteractable;
        internal static Il2CppArrayBase<global::VRCSDK2.VRC_Interactable> allSDK2Interactable;
        internal static Il2CppArrayBase<global::VRC.SDKBase.VRC_Trigger> allTriggers;
        internal static Il2CppArrayBase<global::VRCSDK2.VRC_Trigger> allSDK2Triggers;
        internal static Il2CppArrayBase<global::VRC.SDKBase.VRC_TriggerColliderEventTrigger> allTriggerCol;
        private static global::VRC.SDKBase.VRC_SceneDescriptor SceneDescriptor;
        private static global::VRCSDK2.VRC_SceneDescriptor SDK2SceneDescriptor;
        private static VRCSceneDescriptor SDK3SceneDescriptor;
        internal static HighlightsFXStandalone highlightsFX;
        internal static UdonBehaviour[] udonBehaviours;
        internal static Il2CppArrayBase<UdonSync> udonSync;
        internal static Il2CppArrayBase<UdonManager> udonManagers = Resources.FindObjectsOfTypeAll<UdonManager>();
        internal static Il2CppArrayBase<OnTriggerStayProxy> udonOnTrigger;
        internal static Il2CppArrayBase<OnCollisionStayProxy> udonOnCol;
        internal static Il2CppArrayBase<OnRenderObjectProxy> udonOnRender;
        internal static Il2CppArrayBase<VRCUdonAnalytics> allSDKUdon;
        private static readonly string[] toSkip = new string[] { "PhotoCamera", "MirrorPickup", "ViewFinder", "AvatarDebugConsole", "OscDebugConsole" };
        internal static GameObject funnyrpcobj;
        public static void LoadedScene(int buildindex, string sceneName)
        {
            if (sceneName == "Murder Nevermore")
            {
                MelonCoroutines.Start(WorldHacks.Murder4.InitTheme());
            }
        }
        public static IEnumerator WaitForLocalPlayer()
        {
            DeepConsole.Log("SceneManager", "Waiting for localplayer...");
            while (Player.prop_Player_0 == null)
            {
                yield return null;
            }
            funnyrpcobj = new GameObject("deepclientrpcsht");
            allTriggers = Resources.FindObjectsOfTypeAll<global::VRC.SDKBase.VRC_Trigger>();
            allSDK2Triggers = Resources.FindObjectsOfTypeAll<global::VRCSDK2.VRC_Trigger>();
            allTriggerCol = Resources.FindObjectsOfTypeAll<global::VRC.SDKBase.VRC_TriggerColliderEventTrigger>();
            allInteractable = Resources.FindObjectsOfTypeAll<VRCInteractable>();
            allBaseInteractable = Resources.FindObjectsOfTypeAll<global::VRC.SDKBase.VRC_Interactable>();
            allSDK2Interactable = Resources.FindObjectsOfTypeAll<global::VRCSDK2.VRC_Interactable>();
            sdk2Items = Resources.FindObjectsOfTypeAll<global::VRCSDK2.VRC_Pickup>();
            sdk3Items = Resources.FindObjectsOfTypeAll<VRCPickup>();
            allSyncItems = Resources.FindObjectsOfTypeAll<VRC_ObjectSync>();
            allSDK3SyncItems = Resources.FindObjectsOfTypeAll<VRCObjectSync>();
            allPoolItems = Resources.FindObjectsOfTypeAll<VRCObjectPool>();
            sdk3Items = Object.FindObjectsOfType<VRCPickup>();
            allSyncItems = Enumerable.ToArray<VRC_ObjectSync>(Enumerable.Where<VRC_ObjectSync>(Resources.FindObjectsOfTypeAll<VRC_ObjectSync>(), (VRC_ObjectSync x) => !Enumerable.Any<string>(toSkip, (string y) => y.Contains(x.gameObject.name))));
            allBaseUdonItem = Enumerable.ToArray<global::VRC.SDKBase.VRC_Pickup>(Enumerable.Where<global::VRC.SDKBase.VRC_Pickup>(Resources.FindObjectsOfTypeAll<global::VRC.SDKBase.VRC_Pickup>(), (global::VRC.SDKBase.VRC_Pickup x) => !Enumerable.Any<string>(toSkip, (string y) => y.Contains(x.gameObject.name))));
            SceneDescriptor = Object.FindObjectOfType<global::VRC.SDKBase.VRC_SceneDescriptor>(true);
            SDK2SceneDescriptor = Object.FindObjectOfType<global::VRCSDK2.VRC_SceneDescriptor>(true);
            SDK3SceneDescriptor = Object.FindObjectOfType<VRCSceneDescriptor>(true);
            udonBehaviours = Object.FindObjectsOfType<UdonBehaviour>();
            udonSync = Resources.FindObjectsOfTypeAll<UdonSync>();
            udonManagers = Resources.FindObjectsOfTypeAll<UdonManager>();
            udonOnTrigger = Resources.FindObjectsOfTypeAll<OnTriggerStayProxy>();
            udonOnCol = Resources.FindObjectsOfTypeAll<OnCollisionStayProxy>();
            udonOnRender = Resources.FindObjectsOfTypeAll<OnRenderObjectProxy>();
            allSDKUdon = Resources.FindObjectsOfTypeAll<VRCUdonAnalytics>(); 
            if (highlightsFX == null)
            {
                highlightsFX = Enumerable.FirstOrDefault<HighlightsFXStandalone>(Resources.FindObjectsOfTypeAll<HighlightsFXStandalone>());
            }
            DeepConsole.DebugLog($@"
Total VRC_Trigger:{allTriggers.Count}
Total VRCPickup:{sdk3Items.Count}
Total VRC_TriggerColliderEventTrigger:{allTriggerCol.Count}
Total VRCInteractable:{allInteractable.Count}
Total VRC_ObjectSync:{allSyncItems.ToList().Count}
Total UdonBehaviour:{udonBehaviours.ToList().Count}
Total VRCObjectSync:{allSDK3SyncItems.Count}
Total VRCObjectPool:{allPoolItems.Count}
Total BaseUdonItem:{allBaseUdonItem.ToList().Count}
Total UdonSync:{udonSync.Count}
Total UdonManager:{udonManagers.Count}
Total OnTriggerStayProxy:{udonOnTrigger.Count}
Total OnCollisionStayProxy:{udonOnCol.Count}
Total VRCUdonAnalytics:{allSDK3SyncItems.Count}
Total (SDK2) VRC_Trigger:{allSDK2Triggers.Count}
Total (SDK2) VRC_Interactable:{allSDK2Interactable.Count}
Total (SDK2) VRC_Pickup:{sdk2Items.Count}");
            VrcExtensions.Toast("DeepClient - World","Loaded everything i need. >:3");
        }
    }
}
