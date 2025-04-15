using BepInEx;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using DeepClient.Client;
using DeepClient.Client.Mono;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;

namespace DeepClient
{
    [BepInPlugin("Biscuit", "DeepClient", "1.0")]
    public class EntryPoint : BasePlugin
    {
        internal static new ManualLogSource Log;
        public static bool IsBot = false;
        public override void Load()
        {
            Log = base.Log;
            DeepConsole.Alloc();
            ConfManager.Initialize();
            Client.Misc.SpriteManager.LoadSprite();
            Client.Patching.InitPatch.Start();
            Client.Coroutines.CoroutineManager.Init();
            AddComponent<MainMonoBehaviour>();
            AddComponent<Client.ClientMenu.VRMenu>();
            AddComponent<Client.Mono.FakeVRCPlus>();
        }
        public static void Injectories()
        {

        }
    }
    public class MainMonoBehaviour : MonoBehaviour
    {
        void OnEnable()
        {
            DearImGuiInjection.DearImGuiInjection.Render += Client.GUI.Rendering.RenderGui;
        }
        void Update()
        {
            Client.Module.QOL.KeyBindManager.OnUpdate();
            Client.Module.Exploits.ItemLagger.OnUpdate();
            Client.Module.Exploits.OwnerNameSpoofer.Update();
            Client.Module.Movement.Jetpack.Update();
            Client.Module.Movement.SpinBot.Update();
            Client.Module.Movement.Flight.FlyUpdate();
            Client.Module.Movement.RayCastTP.Update();
            Client.Module.Visuals.OptifineZoom.Update();
            Client.Module.Visuals.ThirdPersonView.Update();
        }
    }
}
