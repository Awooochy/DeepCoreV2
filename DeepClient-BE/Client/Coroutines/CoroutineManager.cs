using DeepClient.Client.Misc.DeepClient.Client.Misc;

namespace DeepClient.Client.Coroutines
{
    internal class CoroutineManager
    {
        public static void Init()
        {
            CustomMenuBG.Init().Start();
            CustomLoadingAudios.Init().Start();
            CustomLoadingScreen.Init().Start();
            CustomVRLoadingOverlay.Init().Start();
        }
    }
}
