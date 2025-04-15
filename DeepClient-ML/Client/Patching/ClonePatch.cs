using System.Reflection;
using HarmonyLib;
using VRC.Core;

namespace DeepCore.Client.Patching
{
    internal class ClonePatch
    {
        public static void Patch()
        {
            EasyPatching.DeepCoreInstance.Patch(typeof(APIUser).GetProperty("allowAvatarCopying").GetSetMethod(), new HarmonyMethod(typeof(ClonePatch).GetMethod("Hook", BindingFlags.Static | BindingFlags.NonPublic)), null, null, null, null);
        }
        private static void Hook(ref bool __0)
        {
            __0 = true;
        }
    }
}
