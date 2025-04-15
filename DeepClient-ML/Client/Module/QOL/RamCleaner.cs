using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Timers;
using MelonLoader;

namespace DeepCore.Client.Module.QOL
{
    internal class RamCleaner
    {
        [DllImport("kernel32.dll")]
        private static extern bool SetProcessWorkingSetSize(IntPtr process, IntPtr minimumWorkingSetSize, IntPtr maximumWorkingSetSize);
        public static Timer clearTimer;
        public static float clearInterval = 120f;
        public static bool useWorkingSetTrim = true;

        public static void StartMyCleaner()
        {
            DeepConsole.Log("OptiRam",$"Interval: {clearInterval} seconds. WorkingSetTrim is {(useWorkingSetTrim ? "enabled" : "disabled")}.");
            clearTimer = new Timer(clearInterval * 1000f);
            clearTimer.Elapsed += ClearTimer_Elapsed;
            clearTimer.AutoReset = true;
            clearTimer.Start();
        }
        public static void forceclear()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            if (useWorkingSetTrim)
            {
                SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, new IntPtr(-1), new IntPtr(-1));
            }
            Protecc.BaseProtecc.IsDebuggerPresent();
        }
        public static void ClearTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (useWorkingSetTrim)
                {
                    SetProcessWorkingSetSize(Process.GetCurrentProcess().Handle, new IntPtr(-1), new IntPtr(-1));
                }
                MelonLogger.Msg("RAM cleared successfully.");
            }
            catch (Exception ex)
            {
                MelonLogger.Error($"Error during RAM clearing: {ex.Message}");
            }
        }
    }
}