using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace DeepCore.Client.Misc
{
    public static class ForceGarbageCollection
    {
        [DllImport("KERNEL32.DLL", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetProcessWorkingSetSize", SetLastError = true)]
        internal static extern bool SetProcessWorkingSetSize32Bit(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);
        [DllImport("KERNEL32.DLL", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetProcessWorkingSetSize", SetLastError = true)]
        internal static extern bool SetProcessWorkingSetSize64Bit(IntPtr pProcess, long dwMinimumWorkingSetSize, long dwMaximumWorkingSetSize);
        public static void RamClear()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            bool flag = Environment.OSVersion.Platform == PlatformID.Win32NT;
            if (flag)
            {
             SetProcessWorkingSetSize32Bit(Process.GetCurrentProcess().Handle, -1, -1);
             VrcExtensions.HudNotif("Cleared GarbageCollector.");
            }
        }
    }
}