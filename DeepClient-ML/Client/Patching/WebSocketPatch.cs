using System;
using WebSocketSharp;

namespace DeepCore.Client.Patching
{
    internal class WebSocketPatch
    {
        public static void Patch()
        {
            EasyPatching.DeepCoreInstance.Patch(typeof(WebSocket).GetMethod("messagec"), EasyPatching.GetLocalPatch<UdonSyncPatch>("OnMSG"), null, null, null, null);
        }
        public static void OnMSGPatch(IntPtr instance, IntPtr __0)
        {
            DeepConsole.LogConsole("Module : WebSocket", $"{instance}:{__0}");
        }
    }
}
