using System.Linq;
using System.Reflection;
using System.Text;
using ExitGames.Client.Photon;
using HarmonyLib;

namespace DeepClient.Client.Patching.Modules
{
    internal class LoadBalancingClienta
    {
        public static bool isdebugtime = false;
        public static void Patch()
        {
            EasyPatching.DeepCoreInstance.Patch(typeof(LoadBalancingClient_Internal).GetMethods().LastOrDefault((MethodInfo x) => x.Name.Equals("OnEvent")), new HarmonyMethod(AccessTools.Method(typeof(LoadBalancingClienta), "OnEvent", null, null)), null, null, null, null);
        }
        internal static bool OnEvent(EventData param_1)
        {
            if (isdebugtime)
            {
                DeepConsole.LogConsole("Photon", "--------------------------------------------------");
                DeepConsole.LogConsole("Photon", $"Event Code:{param_1.Code}");
                DeepConsole.LogConsole("Photon", $"Event type:{param_1.Sender}");
                DeepConsole.LogConsole("Photon", $"Sender:{param_1.Sender}");
                DeepConsole.LogConsole("Photon", $"SenderKey:{param_1.SenderKey}");
                DeepConsole.LogConsole("Photon", $"Parameters:{param_1.Parameters}");
                DeepConsole.LogConsole("Photon", $"Pointer:{param_1.Pointer}");
                DeepConsole.LogConsole("Photon", $"Data:{PrintByteArray(Misc.SerializationUtils.ToByteArray(param_1.CustomData))}");
                DeepConsole.LogConsole("Photon", "--------------------------------------------------");
            }
            if (Module.Photon.ChatBoxLogger.OnEvent(param_1))
            {
                return true;
            }
            return true;
        }
        public static string PrintByteArray(byte[] bytes)
        {
            var sb = new StringBuilder("[");
            foreach (var b in bytes)
            {
                sb.Append(b + ", ");
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
