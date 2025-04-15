using HarmonyLib;
using UnityEngine;

namespace DeepCore.Client.Patching
{
    [HarmonyPatch(typeof(Time), "smoothDeltaTime", MethodType.Getter)]
    internal class TimePatch
    {
        static void Postfix(ref float __result)
        {
            __result = 10f;
        }
    }
}