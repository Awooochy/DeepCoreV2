using System;
using System.IO;
using System.Runtime.InteropServices;
using HarmonyLib;
using MelonLoader;

/*
 * Original source code: https://github.com/knah/ML-UniversalMods/tree/main/NoSteamAtAll
 */

namespace DeepCore.Client.Module.QOL
{
    internal class NoSteamAtAll
    {
        public static void Start()
        {
            var path = MelonUtils.GetGameDataDirectory() + "\\Plugins\\steam_api64.dll";
            if (!File.Exists(path)) path = MelonUtils.GetGameDataDirectory() + "\\Plugins\\x86_64\\steam_api64.dll";
            if (!File.Exists(path)) path = MelonUtils.GetGameDataDirectory() + "\\Plugins\\x86\\steam_api64.dll";
            var library = LoadLibrary(path);
            if (library == IntPtr.Zero)
            {
                DeepConsole.Log("NoSteamAtAll", $"Library load failed; used path: {path}");
                return;
            }
            var names = new[]
            {
                "SteamAPI_Init",
                "SteamAPI_RestartAppIfNecessary",
                "SteamAPI_GetHSteamUser",
                "SteamAPI_RegisterCallback",
                "SteamAPI_UnregisterCallback",
                "SteamAPI_RunCallbacks",
                "SteamAPI_Shutdown"
            };
            var success = false;
            if (ConfManager.ShouldSteamAPI.Value) return;
            foreach (var name in names)
                unsafe
                {
                    var address = GetProcAddress(library, name);
                    if (address == IntPtr.Zero)
                    {
                        DeepConsole.Log("NoSteamAtAll", $"Procedure {name} not found");
                        continue;
                    }

                    MelonUtils.NativeHookAttach((IntPtr)(&address),
                        AccessTools.Method(typeof(NoSteamAtAll), nameof(InitFail)).MethodHandle
                            .GetFunctionPointer());
                    success = true;
                }

            if (success) DeepConsole.Log("NoSteamAtAll", "Disabled SteamAPI");
        }

        public static string OriginalAuthor => "knah";

        [DllImport("kernel32", SetLastError = true, CharSet = CharSet.Ansi)]
        private static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

        public static bool InitFail()
        {
            return false;
        }
    }
}
