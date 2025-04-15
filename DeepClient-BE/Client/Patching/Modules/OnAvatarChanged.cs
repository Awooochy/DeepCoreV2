using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using VRC.SDKBase;

namespace DeepClient.Client.Patching.Modules
{
    internal class OnAvatarChanged
    {
        public static void Patch()
        {
            (from m in typeof(VRCPlayer).GetMethods()
             where m.Name.StartsWith("Method_Private_Void_GameObject_VRC_AvatarDescriptor_")
             select m).ToList<MethodInfo>().ForEach(delegate (MethodInfo m)
             {
                 EasyPatching.DeepCoreInstance.Patch(typeof(VRCPlayer).GetMethod(m.Name), EasyPatching.GetLocalPatch<OnAvatarChanged>("OnAvaLoaded"), null, null, null, null);
             });
        }
        internal static void OnAvaLoaded(GameObject __0, VRC_AvatarDescriptor __1, PlayerNet_Internal __instance)
        {
            try
            {
                if (ConfManager.avatarLogging.Value)
                {
                    var player = __instance.prop_MonoBehaviourPublicAPOb_vOb_l_pBoOb1BoUnique_0;
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
                        DeepConsole.LogConsole("AvaLogger", text);
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