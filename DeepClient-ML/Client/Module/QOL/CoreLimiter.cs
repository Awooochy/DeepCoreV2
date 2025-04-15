using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using MelonLoader;

namespace DeepCore.Client.Module.QOL
{
    internal class CoreLimiter
    {
        private const string CoreLimiterPrefCategory = "CoreLimiter";
        private const string MaxCoresPref = "MaxCores";
        private const string SkipHyperThreadsPref = "SkipHyperThreads";

        public static void Start()
        {
            MelonPreferences.CreateCategory(CoreLimiterPrefCategory, "Core Limiter");
            MelonPreferences.CreateEntry(CoreLimiterPrefCategory, MaxCoresPref, 4, "Maximum cores");
            MelonPreferences.CreateEntry(CoreLimiterPrefCategory, SkipHyperThreadsPref, true, "Don't use both threads of a core");
            DeepConsole.Log("CoreLimiter", $"Have {Environment.ProcessorCount} processor cores.");
            ApplyAffinity();
        }
        public static void ApplyAffinity()
        {
            int processorCount = Environment.ProcessorCount;
            long mask = 0;

            int targetNumCores = MelonPreferences.GetEntryValue<int>(CoreLimiterPrefCategory, MaxCoresPref);
            int targetBit = processorCount - 1;
            int bitStep = MelonPreferences.GetEntryValue<bool>(CoreLimiterPrefCategory, SkipHyperThreadsPref) ? 2 : 1;

            for (int i = 0; i < targetNumCores && targetBit >= 0; i++)
            {
                mask |= 1L << targetBit;
                targetBit -= bitStep;
            }

            IntPtr processHandle = Process.GetCurrentProcess().Handle;
            DeepConsole.Log("CoreLimiter",$"Assigning affinity mask: {mask}.");
            SetProcessAffinityMask(processHandle, new IntPtr(mask));
        }

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern bool SetProcessAffinityMask(IntPtr hProcess, IntPtr dwProcessAffinityMask);
    }
}