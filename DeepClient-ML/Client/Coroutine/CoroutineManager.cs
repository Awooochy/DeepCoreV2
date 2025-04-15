using MelonLoader;

namespace DeepCore.Client.Coroutine
{
    internal class CoroutineManager
    {
        public static void Init()
        {
            DeepConsole.Log("CoroutineManager", "Starting Coroutines...");
            MelonCoroutines.Start(Misc.VrcExtensions.SetMicColor());
            MelonCoroutines.Start(CustomStandardPopup.Init());
            MelonCoroutines.Start(CustomLoadingScreen.Init());
            MelonCoroutines.Start(CustomVRLoadingOverlay.Init());
            MelonCoroutines.Start(CustomMenuBG.Init());
        }
    }
}
