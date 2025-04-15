using UnityEngine.XR;

namespace DeepCore.Client.Misc
{
    internal class ERPChecker
    {
        public static void IsMyUserERping()
        {
            if(XRSettings.isDeviceActive)
            {
                Entry.IsInVR = true;
                DeepConsole.Log("Core", "Stop erping :(");
            }
        }
    }
}
