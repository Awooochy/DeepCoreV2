using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using AmplitudeSDKWrapper;
using Depthkit;
using ExitGames.Client.Photon;
using HarmonyLib;
using MelonLoader;
using UnhollowerBaseLib;
using UnityEngine;

namespace DeepCore.Client.Patching
{
    internal class SpooferPatch
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        internal static extern IntPtr LoadLibrary(string lpFileName);
        public static string newHWID = string.Empty;
        public static bool fakepingenabled;
        public static int FakepingValue = 69;
        public static bool Fakefpsenabled = false;
        public static int FakefpsValue = 69;

        public static void InitSpoofs()
        {
            try
            {
                EasyPatching.DeepCoreInstance.Patch(typeof(SystemInfo).GetProperty("deviceUniqueIdentifier").GetGetMethod(), new HarmonyMethod(AccessTools.Method(typeof(SpooferPatch), nameof(FakeHWID))));
                EasyPatching.DeepCoreInstance.Patch(typeof(AmplitudeWrapper).GetMethod("PostEvents"), new HarmonyMethod(AccessTools.Method(typeof(SpooferPatch), nameof(VoidPatch))));
                SafetyPatch();
            }
            catch (Exception ERROR)
            {
                DeepConsole.Log("Spoofer", $"Could not patch Analystics failed\n{ERROR}");
            }
        }
        private static bool VoidPatch()
        {
            return false;
        }
        private static bool FakeHWID(ref string __result)
        {
            if (newHWID == string.Empty)
            {
                newHWID = KeyedHashAlgorithm.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}A-{1}{2}-{3}{4}-{5}{6}-3C-1F", new object[]
                {
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9) }))).Select(delegate (byte x)
                    {
                        byte b = x;
                        return b.ToString("x2");
                    }).Aggregate((string x, string y) => x + y);
                DeepConsole.Log("Spoofer", $"HWID:{newHWID}");
            }
            __result = newHWID;
            return false;
        }
        public unsafe static void SafetyPatch()
        {
            IntPtr intPtr = IntPtr.Zero;
            try
            {
                intPtr = LoadLibrary(MelonUtils.GetGameDataDirectory() + "\\Plugins\\x86_64\\steam_api64.dll");
            }
            catch (Exception e)
            {
                DeepConsole.Log("Spoofer", $"Can't Spoof Steam");
            }
            if (intPtr == IntPtr.Zero)
            DeepConsole.Log("Spoofer", $"Can't Spoof Steam");
            IntPtr procAddress = GetProcAddress(intPtr, "SteamAPI_Init");
            IntPtr procAddress2 = GetProcAddress(intPtr, "SteamAPI_RestartAppIfNecessary");
            IntPtr procAddress3 = GetProcAddress(intPtr, "SteamAPI_GetHSteamUser");
            IntPtr procAddress4 = GetProcAddress(intPtr, "SteamAPI_RegisterCallback");
            IntPtr procAddress5 = GetProcAddress(intPtr, "SteamAPI_UnregisterCallback");
            IntPtr procAddress6 = GetProcAddress(intPtr, "SteamAPI_RunCallbacks");
            IntPtr procAddress7 = GetProcAddress(intPtr, "SteamAPI_Shutdown");
            try
            {
                if (true)
                {
                    MelonUtils.NativeHookAttach((IntPtr)((void*)(&procAddress)), AccessTools.Method(typeof(SpooferPatch), nameof(SteamSpoof), null, null).MethodHandle.GetFunctionPointer());
                    MelonUtils.NativeHookAttach((IntPtr)((void*)(&procAddress2)), AccessTools.Method(typeof(SpooferPatch), nameof(SteamSpoof), null, null).MethodHandle.GetFunctionPointer());
                    MelonUtils.NativeHookAttach((IntPtr)((void*)(&procAddress3)), AccessTools.Method(typeof(SpooferPatch), nameof(SteamSpoof), null, null).MethodHandle.GetFunctionPointer());
                    MelonUtils.NativeHookAttach((IntPtr)((void*)(&procAddress4)), AccessTools.Method(typeof(SpooferPatch), nameof(SteamSpoof), null, null).MethodHandle.GetFunctionPointer());
                    MelonUtils.NativeHookAttach((IntPtr)((void*)(&procAddress5)), AccessTools.Method(typeof(SpooferPatch), nameof(SteamSpoof), null, null).MethodHandle.GetFunctionPointer());
                    MelonUtils.NativeHookAttach((IntPtr)((void*)(&procAddress6)), AccessTools.Method(typeof(SpooferPatch), nameof(SteamSpoof), null, null).MethodHandle.GetFunctionPointer());
                    MelonUtils.NativeHookAttach((IntPtr)((void*)(&procAddress7)), AccessTools.Method(typeof(SpooferPatch), nameof(SteamSpoof), null, null).MethodHandle.GetFunctionPointer());
                }
            }
            catch
            {
                DeepConsole.Log("Spoofer", $"Cant Spoof Steam");
            }
            IntPtr intPtr2 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetDeviceModel");
            MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr2)), AccessTools.Method(typeof(SpooferPatch), "FakeModel", null, null).MethodHandle.GetFunctionPointer());
            IntPtr intPtr3 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetDeviceName");
            MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr3)), AccessTools.Method(typeof(SpooferPatch), "FakeName", null, null).MethodHandle.GetFunctionPointer());
            IntPtr intPtr4 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetGraphicsDeviceName");
            MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr4)), AccessTools.Method(typeof(SpooferPatch), "FakeGBU", null, null).MethodHandle.GetFunctionPointer());
            IntPtr intPtr5 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetGraphicsDeviceID");
            MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr5)), AccessTools.Method(typeof(SpooferPatch), "FakeGBUID", null, null).MethodHandle.GetFunctionPointer());
            IntPtr intPtr6 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetProcessorType");
            MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr6)), AccessTools.Method(typeof(SpooferPatch), "FakeProcessor", null, null).MethodHandle.GetFunctionPointer());
            IntPtr intPtr7 = IL2CPP.il2cpp_resolve_icall("UnityEngine.SystemInfo::GetOperatingSystem");
            MelonUtils.NativeHookAttach((IntPtr)((void*)(&intPtr7)), AccessTools.Method(typeof(SpooferPatch), "FakeOS", null, null).MethodHandle.GetFunctionPointer());
        }
        public static IntPtr FakeModel() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(Motherboards[new System.Random().Next(0, Motherboards.Length)])).Pointer;
        public static IntPtr FakeName() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp("DESKTOP-" + RandomString(7))).Pointer;
        public static IntPtr FakeGBU() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(PBU[new System.Random().Next(0, PBU.Length)])).Pointer;
        public static IntPtr FakeGBUID() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(RandomString(12))).Pointer;
        public static IntPtr FakeProcessor() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(CPU[new System.Random().Next(0, CPU.Length)])).Pointer;
        public static IntPtr FakeOS() => new UnityEngine.Object(IL2CPP.ManagedStringToIl2Cpp(OS[new System.Random().Next(0, OS.Length)])).Pointer;


        private static System.Random random = new System.Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        private static bool SteamSpoof()
        {
            return false;
        }
        private static bool VoidPatchTrue(bool __result)
        {
            __result = true;
            return false;
        }
        private static bool VoidPatchFalse(bool __result)
        {
            __result = false;
            return false;
        }
        private static bool PatchFPS(ref float __result)
        {
            if (Fakefpsenabled)
                return true;
            __result = 1f / Math.Max(1f, FakefpsValue);
            return false;
        }
        private static bool PatchPing(ref int __result)
        {
            if (fakepingenabled)
                return true;
            __result = Math.Max(0, FakepingValue);
            return false;
        }
        private static bool RoundTrip(ref int __result)
        {
            if (fakepingenabled)
            {
                int ping = 1;
                float sinValue = Mathf.Sin(Time.realtimeSinceStartup / 14210f);

                if (sinValue < 0)
                {
                    sinValue = -sinValue;
                }

                float cosValue = Mathf.Cos(Time.realtimeSinceStartup / 5);

                if (cosValue < 0)
                {
                    cosValue = -cosValue;
                }

                float sincos = sinValue * cosValue;

                ping = (int)(ping + ping / -23193 * sincos);

                __result = ping;
                return false;
            }
            return false;
        }
        private static string[] PBU = new string[]
        {
            "MSI Radeon RX 6900 XT GAMING Z TRIO 16GB",
            "Gigabyte Radeon RX 6700 XT Gaming OC 12GB",
            "ASUS DUAL GeForce RTX 2060 6GB OC EVO",
            "Palit GeForce GTX 1050 Ti StormX 4GB",
            "MSI GeForce GTX 1650 D6 Ventus XS OCV2 12GB OC",
            "ASUS GOLD20TH GTX 980 Ti Platinum",
            "ASUS TUF GeForce RTX 3060 12GB OC Gaming",
            "NVIDIA Quadro RTX 4000 8GB",
            "NVIDIA GeForce GTX 1080 Ti",
            "NVIDIA GeForce GTX 1080",
            "NVIDIA GeForce GTX 1070",
            "NVIDIA GeForce GTX 1060 6GB",
            "NVIDIA GeForce GTX 980 Ti"
        };
        private static string[] CPU = new string[]
        {
            "AMD Ryzen 5 3600",
            "AMD Ryzen 7 3700X",
            "AMD Ryzen 7 5800X",
            "AMD Ryzen 9 5900X",
            "AMD Ryzen 9 3900X 12-Core Processor",
            "INTEL CORE I9-10900K",
            "INTEL CORE I7-10700K",
            "INTEL CORE I9-9900K",
            "Intel Core i7-8700K"
        };
        private static string[] Motherboards = new string[]
        {
            "B550 AORUS PRO (Gigabyte Technology Co., Ltd.)",
            "Gigabyte B450M DS3H",
            "Asus AM4 TUF Gaming X570-Plus",
            "ASRock Z370 Taichi"
        };
        private static string[] OS = new string[]
        {
            "Windows 10  (10.0.0) 64bit",
            "Windows 10  (10.0.0) 32bit",
            "Windows 8  (10.0.0) 64bit",
            "Windows 8  (10.0.0) 32bit",
            "Windows 7  (10.0.0) 64bit",
            "Windows 7  (10.0.0) 32bit",
            "Windows Vista 64bit",
            "Windows Vista 32bit",
            "Windows XP 64bit",
            "Windows XP 32bit"
        };
    }
}