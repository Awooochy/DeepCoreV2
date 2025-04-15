using System;
using VRC.SDK3.Video.Components;
using VRC.SDKBase;

namespace DeepCore.Client.Patching
{
    internal class VideoPlayerPatch
    {
        public static void Patch()
        {
            EasyPatching.EasyPatchMethodPost(typeof(VRCUnityVideoPlayer), "LoadURL", typeof(VideoPlayerPatch), "Patch");
        }
        public static void Patch(IntPtr instance, IntPtr __0)
        {
            VRCUrl vrcurl = (__0 == IntPtr.Zero) ? null : new VRCUrl(__0);
            if (vrcurl == null)
            {
                return;
            }
            if (!ConfManager.VideoPlayerURLLogger.Value)
            {
                DeepConsole.Log("VideoPlayer",$"Video loading from:{vrcurl.url}");
                return;
            }
            DeepConsole.Log("VideoPlayer",$"Video tried loading from:{vrcurl.url}");
        }
    }
}
