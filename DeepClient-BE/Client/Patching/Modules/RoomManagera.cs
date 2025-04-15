using VRC.Core;

namespace DeepClient.Client.Patching.Modules
{
    internal class RoomManagera
    {
        public static void Patch()
        {
            EasyPatching.EasyPatchMethodPost(typeof(RoomManager_Internal), "Method_Public_Static_Boolean_ApiWorld_ApiWorldInstance_String_Int32_0", typeof(RoomManagera), "EnterWorldPatch");
        }
        private static void EnterWorldPatch(ApiWorld __0, ApiWorldInstance __1)
        {
            if (ConfManager.OwnerSpoof.Value)
            {
                Module.Exploits.OwnerNameSpoofer.Enabled = true;
            }
            bool flag = __0 == null || __1 == null;
            if (!flag)
            {
                DeepConsole.Log("RoomManager", $"Joining {RoomManager_Internal.field_Internal_Static_ApiWorld_0.name} by {RoomManager_Internal.field_Internal_Static_ApiWorld_0.authorName}...");
                if (ConfManager.AntiAvatarScallingdisabler.Value)
                {
                    if (__0.tags.Contains("feature_avatar_scaling_disabled"))
                    {
                        __0.tags.Remove("feature_avatar_scaling_disabled");
                    }
                }
            }
        }
    }
}