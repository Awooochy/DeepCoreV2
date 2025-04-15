using HarmonyLib;
using UnityEngine;

namespace DeepClient.Client.Patching.Modules
{
    [HarmonyPatch(typeof(HighlightsFXStandalone), nameof(HighlightsFXStandalone.Awake))]
    [System.Obsolete]
    class HighlightColor
    {
        static void Postfix(HighlightsFXStandalone __instance)
        {
            __instance.highlightColor = Color.white;
        }
    }
}