using VRC.SDKBase;

namespace DeepClient.Client.Patching.Modules
{
    internal class TriggerWorld
    {
        public static bool BlockWTriggers = false;
        public static void Patch()
        {
            EasyPatching.DeepCoreInstance.Patch(typeof(VRC_EventHandler).GetMethod("InternalTriggerEvent"), EasyPatching.GetLocalPatch<TriggerWorld>("OnTriggerWorld"), null, null, null, null);
        }
        internal static bool OnTriggerWorld(ref VRC_EventHandler.VrcEvent __0, ref VRC_EventHandler.VrcBroadcastType __1, ref int __2)
        {
            try
            {
                if (ConfManager.blockWorldTriggers.Value)
                {
                    __1 = (VRC_EventHandler.VrcBroadcastType)4;
                }
            }
            catch
            {
            }
            return true;
        }
    }
}