using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DeepClient.Client.Module.QOL
{
    internal class CoreLimiter
    {
        private const string CoreLimiterPrefCategory = "CoreLimiter";
        private const string MaxCoresPref = "MaxCores";
        private const string SkipHyperThreadsPref = "SkipHyperThreads";

        public static void Start()
        {
            DeepConsole.Log("CoreLimiter", $"Have {Environment.ProcessorCount} processor cores.");
            ApplyAffinity();
        }
        public static void ApplyAffinity()
        {
            int processorCount = Environment.ProcessorCount;
            long mask = 0;

            int targetNumCores = 4;
            int targetBit = processorCount - 1;
            int bitStep = true ? 2 : 1;

            for (int i = 0; i < targetNumCores && targetBit >= 0; i++)
            {
                mask |= 1L << targetBit;
                targetBit -= bitStep;
            }

            IntPtr processHandle = Process.GetCurrentProcess().Handle;
            DeepConsole.Log("CoreLimiter", $"Assigning affinity mask: {mask}.");
            SetProcessAffinityMask(processHandle, new IntPtr(mask));
        }

        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall)]
        private static extern bool SetProcessAffinityMask(IntPtr hProcess, IntPtr dwProcessAffinityMask);
    }
}