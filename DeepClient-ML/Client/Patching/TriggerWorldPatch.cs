using VRC.SDKBase;

namespace DeepCore.Client.Patching
{
    internal class TriggerWorldPatch
    {
        [System.Obsolete]
        public static void Patch()
        {
            EasyPatching.DeepCoreInstance.Patch(typeof(VRC_EventHandler).GetMethod("InternalTriggerEvent"), EasyPatching.GetLocalPatch<TriggerWorldPatch>("OnTriggerWorld"), null, null, null, null);
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
