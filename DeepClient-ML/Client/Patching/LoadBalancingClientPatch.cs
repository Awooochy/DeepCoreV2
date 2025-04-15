using System.Linq;
using System.Reflection;
using ExitGames.Client.Photon;
using HarmonyLib;
using Photon.Realtime;

namespace DeepCore.Client.Patching
{
    internal class LoadBalancingClientPatch
    {
        public static void Patch()
        {
            EasyPatching.DeepCoreInstance.Patch(typeof(LoadBalancingClient).GetMethods().LastOrDefault((MethodInfo x) => x.Name.Equals("OnEvent")), new HarmonyMethod(AccessTools.Method(typeof(LoadBalancingClientPatch), "OnEvent", null, null)), null, null, null, null);
        }
        internal static bool OnEvent(EventData param_1)
        {
            return Module.Photon.PhtonManagerUtils.PhotonEvent(param_1);
        }
    }
}
