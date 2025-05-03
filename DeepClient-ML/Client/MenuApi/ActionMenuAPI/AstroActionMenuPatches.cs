namespace AstroClient.MenuApi.ActionMenuAPI
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;
    using ClientActions;
    using HarmonyLib;
    using Helpers;
    using Managers;
    using MelonLoader;
    using xAstroBoy.Patching;

    internal class AstroActionMenuPatchesEvents : AstroEvents
    {

        internal override void ExecutePriorityPatches()
        {
            Execute();
        }
        private static void Execute()
        {
            try
            {
                AstroActionMenuPatches.PatchAll();
            }
            catch (Exception e)
            {
                Log.Error($"Patching failed with exception: {e.Message}");
            }
        }
    }


    [SuppressMessage("ReSharper", "Unity.IncorrectMonoBehaviourInstantiation")]
    [SuppressMessage("ReSharper", "InconsistentNaming")]

    internal static class AstroActionMenuPatches
    {

        [System.Reflection.ObfuscationAttribute(Feature = "HarmonyGetPatch")]
        private static HarmonyMethod GetPatch(string name)
        {
            return new HarmonyMethod(typeof(AstroActionMenuPatches).GetMethod(name, BindingFlags.Static | BindingFlags.NonPublic));
        }


        public static List<PedalStruct>
            configPagePre = new(),
            configPagePost = new(),
            emojisPagePre = new(),
            emojisPagePost = new(),
            expressionPagePre = new(),
            expressionPagePost = new(),
            sdk2ExpressionPagePre = new(),
            sdk2ExpressionPagePost = new(),
            mainPagePre = new(),
            mainPagePost = new(),
            nameplatesPagePre = new(),
            nameplatesPagePost = new(),
            //nameplatesVisibilityPagePre = new(),
            //nameplatesVisibilityPagePost = new(),
            //nameplatesSizePagePre = new(),
            //nameplatesSizePagePost = new(),
            optionsPagePre = new(),
            optionsPagePost = new();







        private static readonly List<string>
            openConfigPageKeyWords =
                new() { "Avatar Overlay" }; // new(new[] {"Menu Size", "Menu Opacity"}); Those are in sub-functions now

        private static readonly List<string> openMainPageKeyWords = new(new[] { "Options", "Emojis" });
        private static readonly List<string> openEmojisPageKeyWords = new(new[] { " ", "_" });

        private static readonly List<string>
            openExpressionMenuKeyWords = new(new[] { "Reset Avatar", "Release Poses" });

        private static readonly List<string> openOptionsPageKeyWords = new(new[] { "Chatbox", "Nameplates" });
        private static readonly List<string> openSDK2ExpressionPageKeyWords = new(new[] { "EMOTE{0}" });

        private static readonly List<string> openNameplatesPageKeyWords = new(new[] { "Visibility", "Size" });

        private static readonly List<string> openNameplatesVisibilityPageKeyWords =
            new(new[] { "Nameplates Shown", "Icons Only", "Nameplates Hidden" });

        private static readonly List<string> openNameplatesSizePageKeyWords =
            new(new[] { "Large", "Medium", "Normal", "Small", "Tiny" });

        private static readonly List<string> openPhysbonesSettingsPageWords = new()
        {
            "None", "PhysBones Proximity", "PhysBones", "Contacts"
        };



        public static void PatchAll()
        {

            // Il2Cpp.MonoBehaviourPublicIPhysBoneDebugDrawerObSiObCoSiLiObCo1SiUnique.EnumNPublicSealedvaNoPhCoPh5vUnique
            //PatchMethod(openExpressionMenuKeyWords, nameof(OpenExpressionMenuPre), nameof(OpenExpressionMenuPost));
            //PatchMethod(openConfigPageKeyWords, nameof(OpenConfigPagePre), nameof(OpenConfigPagePost));
            //PatchMethod(openMainPageKeyWords, nameof(OpenMainPagePre), nameof(OpenMainPagePost));
            //PatchMethod(openEmojisPageKeyWords, nameof(OpenEmojisPagePre), nameof(OpenEmojisPagePost));
            //PatchMethod(openNameplatesPageKeyWords, nameof(OpenNameplatesPagePre), nameof(OpenNameplatesPagePost));
            //PatchMethod(openNameplatesSizePageKeyWords, nameof(OpenNameplatesSizePre), nameof(OpenNameplatesSizePost));
            //PatchMethod(openNameplatesVisibilityPageKeyWords, nameof(OpenNameplatesVisibilityPre), nameof(OpenNameplatesVisibilityPost));
            //PatchMethod(openSDK2ExpressionPageKeyWords, nameof(OpenSDK2ExpressionPre), nameof(OpenSDK2ExpressionPost));
            //PatchMethod(openOptionsPageKeyWords, nameof(OpenOptionsPre), nameof(OpenOptionsPost));

            new AstroPatch(typeof(ActionMenu).GetMethod(nameof(ActionMenu.Method_Public_Void_PDM_13)), GetPatch(nameof(OpenExpressionMenuPre)),GetPatch(nameof(OpenExpressionMenuPost)));
            new AstroPatch(typeof(ActionMenu).GetMethod(nameof(ActionMenu.Method_Private_Void_PDM_9)), GetPatch(nameof(OpenConfigPagePre)), GetPatch(nameof(OpenConfigPagePost)));
            new AstroPatch(typeof(ActionMenu).GetMethod(nameof(ActionMenu.Method_Public_Void_PDM_4)), GetPatch(nameof(OpenMainPagePre)), GetPatch(nameof(OpenMainPagePost)));
            new AstroPatch(typeof(ActionMenu).GetMethod(nameof(ActionMenu.Method_Private_Void_PDM_4)), GetPatch(nameof(OpenEmojisPagePre)), GetPatch(nameof(OpenEmojisPagePost)));
            new AstroPatch(typeof(ActionMenu).GetMethod(nameof(ActionMenu.Method_Public_Void_PDM_1)), GetPatch(nameof(OpenNameplatesPagePre)), GetPatch(nameof(OpenNameplatesPagePost)));
            //new AstroPatch(typeof(ActionMenu).GetMethod(nameof(ActionMenu.OnEnable)), GetPatch(nameof(OpenNameplatesSizePre)), GetPatch(nameof(OpenNameplatesSizePost)));
            //new AstroPatch(typeof(ActionMenu).GetMethod(nameof(ActionMenu.OnEnable)), GetPatch(nameof(OpenNameplatesVisibilityPre)), GetPatch(nameof(OpenNameplatesVisibilityPost)));
            new AstroPatch(typeof(ActionMenu).GetMethod(nameof(ActionMenu.Method_Public_Void_PDM_10)), GetPatch(nameof(OpenSDK2ExpressionPre)), GetPatch(nameof(OpenSDK2ExpressionPost)));
            new AstroPatch(typeof(ActionMenu).GetMethod(nameof(ActionMenu.Method_Private_Void_PDM_0)), GetPatch(nameof(OpenOptionsPre)), GetPatch(nameof(OpenOptionsPost)));


            MelonLogger.Msg("Patches Applied");
        }

        private static void OpenConfigPagePre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(configPagePre, __instance);
        }

        private static void OpenConfigPagePost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(configPagePost, __instance);
        }

        private static void OpenMainPagePre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(mainPagePre, __instance);
        }

        private static void OpenMainPagePost(ActionMenu __instance)
        {
            if (ModsFolderManager.mods.Count > 0) ModsFolderManager.AddMainPageButton(__instance);
            Utilities.AddPedalsInList(mainPagePost, __instance);
        }

        private static void OpenEmojisPagePre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(emojisPagePre, __instance);
        }

        private static void OpenEmojisPagePost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(emojisPagePost, __instance);
        }

        private static void OpenExpressionMenuPre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(expressionPagePre, __instance);
        }

        private static void OpenExpressionMenuPost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(expressionPagePost, __instance);
        }

        private static void OpenNameplatesPagePre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(nameplatesPagePre, __instance);
        }

        private static void OpenNameplatesPagePost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(nameplatesPagePost, __instance);
        }

        //private static void OpenNameplatesVisibilityPre(ActionMenu __instance)
        //{
        //    Utilities.AddPedalsInList(nameplatesVisibilityPagePre, __instance);
        //}
        //
        //private static void OpenNameplatesVisibilityPost(ActionMenu __instance)
        //{
        //    Utilities.AddPedalsInList(nameplatesVisibilityPagePost, __instance);
        //}
        //
        //private static void OpenNameplatesSizePre(ActionMenu __instance)
        //{
        //    Utilities.AddPedalsInList(nameplatesSizePagePre, __instance);
        //}
        //
        //private static void OpenNameplatesSizePost(ActionMenu __instance)
        //{
        //    Utilities.AddPedalsInList(nameplatesSizePagePost, __instance);
        //}

        private static void OpenOptionsPre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(optionsPagePre, __instance);
        }

        private static void OpenOptionsPost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(optionsPagePost, __instance);
        }

        private static void OpenSDK2ExpressionPre(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(sdk2ExpressionPagePre, __instance);
        }

        private static void OpenSDK2ExpressionPost(ActionMenu __instance)
        {
            Utilities.AddPedalsInList(sdk2ExpressionPagePost, __instance);
        }

        //private static MethodInfo FindAMMethod(List<string> keywords)
        //{
        //    return typeof(ActionMenu).GetMethods()
        //        .First(m => m.Name.StartsWith("Method") && XRefExts.CheckXref(m, keywords));
        //}

        //private static void PatchMethod(List<string> keywords, string preName, string postName)
        //{
        //    try
        //    {
        //        new AstroPatch(FindAMMethod(keywords),
        //            new HarmonyLib.HarmonyMethod(typeof(AstroActionMenuPatches).GetMethod(preName)),
        //            new HarmonyLib.HarmonyMethod((typeof(AstroActionMenuPatches).GetMethod(postName))));
        //    }
        //    catch (Exception e)
        //    {
        //        MelonLogger.Warning($"Failed to Patch Method: {preName} <-> {postName} with {string.Join(", ", keywords)}: {e}");
        //    }
        //}
    }
}