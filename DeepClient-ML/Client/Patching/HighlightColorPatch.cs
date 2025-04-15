using Harmony;
using UnityEngine;

namespace DeepCore.Client.Patching
{
    internal class HighlightColorPatch
    {
        [HarmonyPatch(typeof(HighlightsFXStandalone), nameof(HighlightsFXStandalone.Awake))]
        [System.Obsolete]
        class HighlightsFXStandalonePatch
        {
            static void Postfix(HighlightsFXStandalone __instance)
            {
                __instance.highlightColor = Color.white;
            }
        }
    }
}
