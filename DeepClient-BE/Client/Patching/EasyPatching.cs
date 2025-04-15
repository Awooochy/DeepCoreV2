using System;
using System.Reflection;
using HarmonyLib;

namespace DeepClient.Client.Patching
{
    internal class EasyPatching
    {
        public static HarmonyLib.Harmony DeepCoreInstance = new HarmonyLib.Harmony("DeePatch");
        public static void EasyPatchPropertyPost(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname)
        {
            DeepCoreInstance.Patch(AccessTools.Property(inputclass, InputMethodName).GetMethod, null, new HarmonyMethod(outputclass, outputmethodname, null), null, null, null);
        }
        public static void EasyPatchPropertyPre(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname)
        {
            DeepCoreInstance.Patch(AccessTools.Property(inputclass, InputMethodName).GetMethod, new HarmonyMethod(outputclass, outputmethodname, null), null, null, null, null);
        }
        public static void EasyPatchMethodPre(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname)
        {
            DeepCoreInstance.Patch(inputclass.GetMethod(InputMethodName), new HarmonyMethod(AccessTools.Method(outputclass, outputmethodname, null, null)), null, null, null, null);
        }
        public static void EasyPatchMethodPost(Type inputclass, string InputMethodName, Type outputclass, string outputmethodname)
        {
            DeepCoreInstance.Patch(inputclass.GetMethod(InputMethodName), null, new HarmonyMethod(AccessTools.Method(outputclass, outputmethodname, null, null)), null, null, null);
        }

        [Obsolete]
        internal static HarmonyMethod GetLocalPatch<T>(string name)
        {
            return new HarmonyMethod(typeof(T).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));
        }
    }
}