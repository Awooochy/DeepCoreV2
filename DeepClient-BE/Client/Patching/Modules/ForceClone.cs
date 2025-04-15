using System.Reflection;
using HarmonyLib;
using VRC.Core;

namespace DeepClient.Client.Patching.Modules
{
    internal class ForceClone
    {
        public static void Patch()
        {
            EasyPatching.DeepCoreInstance.Patch(typeof(APIUser).GetProperty("allowAvatarCopying").GetSetMethod(), new HarmonyMethod(typeof(ForceClone).GetMethod("Hook", BindingFlags.Static | BindingFlags.NonPublic)), null, null, null, null);
        }
        private static void Hook(ref bool __0)
        {
            __0 = true;
        }
    }
}
