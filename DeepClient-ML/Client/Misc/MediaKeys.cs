using System;
using System.Runtime.InteropServices;

namespace DeepCore.Client.Misc
{
    internal class MediaKeys
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);


        [DllImport("KERNEL32.DLL", CallingConvention = CallingConvention.StdCall, EntryPoint = "SetProcessWorkingSetSize", SetLastError = true)]
        public static extern bool SetProcessWorkingSetSize32Bit(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        public static void PlayPause()
        {
            keybd_event(179, 179, 0, 0);
            keybd_event(179, 179, KEYEVENTF_KEYUP, 0);
        }
        public static void PrevTrack()
        {
            keybd_event(177, 177, 0, 0);
            keybd_event(177, 177, KEYEVENTF_KEYUP, 0);
        }
        public static void NextTrack()
        {
            keybd_event(176, 176, 0, 0);
            keybd_event(176, 176, KEYEVENTF_KEYUP, 0);
        }
        public static void Stop()
        {
            keybd_event(178, 178, 0, 0);
            keybd_event(178, 178, KEYEVENTF_KEYUP, 0);
        }
        public static void VolumeUp()
        {
            keybd_event(175, 175, 0, 0);
            keybd_event(175, 175, KEYEVENTF_KEYUP, 0);
        }
        public static void VolumeDown()
        {
            keybd_event(174, 174, 0, 0);
            keybd_event(174, 174, KEYEVENTF_KEYUP, 0);
        }
        public static void VolumeMute()
        {
            keybd_event(173, 173, 0, 0);
            keybd_event(173, 173, KEYEVENTF_KEYUP, 0);
        }
        private static readonly int KEYEVENTF_KEYUP = 2;
    }
}
