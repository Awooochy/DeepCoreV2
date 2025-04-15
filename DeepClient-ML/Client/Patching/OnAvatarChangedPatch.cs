using System;
using System.Linq;
using System.Net.Configuration;
using System.Reflection;
using DeepCore.Client.Misc;
using UnityEngine;
using VRC;
using VRC.SDKBase;

namespace DeepCore.Client.Patching
{
    internal class OnAvatarChangedPatch
    {
        public static void Patch()
        {
            (from m in typeof(VRCPlayer).GetMethods()
             where m.Name.StartsWith("Method_Private_Void_GameObject_VRC_AvatarDescriptor_")
             select m).ToList<MethodInfo>().ForEach(delegate (MethodInfo m)
             {
                 EasyPatching.DeepCoreInstance.Patch(typeof(VRCPlayer).GetMethod(m.Name), EasyPatching.GetLocalPatch<OnAvatarChangedPatch>("OnAvaLoaded"), null, null, null, null);
             });
        }
        [Obsolete]
        internal static void OnAvaLoaded(GameObject __0, VRC_AvatarDescriptor __1, VRCPlayer __instance)
        {
            try
            {
                if (ConfManager.avatarLogging.Value)
                {
                    Player player = __instance._player;
                    string text = string.Concat(new string[]
                    {
                        "\nUser: (",
                        player.field_Private_VRCPlayerApi_0.displayName,
                        ")\nswitched to avatar: ",
                        player.prop_ApiAvatar_0.name,
                        "(",
                        player.prop_ApiAvatar_0.id,
                        ")\nAuthor: (",
                        player.prop_ApiAvatar_0.authorName,
                        ") \nCreated: (",
                        player.prop_ApiAvatar_0.created_at.ToString(),
                        ") \nLast Updated: (",
                        player.prop_ApiAvatar_0.updated_at.ToString(),
                        ")"
                    });
                    if (!(last_log == text))
                    {
                        last_log = text;
                        AvatarLoggerHandler.Log(player.prop_ApiAvatar_0);
                        DeepConsole.LogConsole("AvaLogger",text);
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
        private static string last_log;
    }
}