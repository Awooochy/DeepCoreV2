using VRC;
using VRC.Networking;

namespace DeepCore.Client.Patching
{
    internal class UdonSyncPatch
    {
        public static void Patch()
        {
            EasyPatching.DeepCoreInstance.Patch(typeof(UdonSync).GetMethod("UdonSyncRunProgramAsRPC"), EasyPatching.GetLocalPatch<UdonSyncPatch>("OnUdon"), null, null, null, null);
        }
        internal static bool OnUdon(string __0, Player __1)
        {
            if (ConfManager.udonLogger.Value)
            {
                DeepConsole.Log("UdonSync", "Type: " + __0 + " | From " + __1.field_Private_VRCPlayerApi_0.displayName);
            }
            return !ConfManager.antiUdon.Value;
        }
    }
}