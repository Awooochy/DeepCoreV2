using System;
using System.Diagnostics;
using System.Media;
using System.Runtime.InteropServices;

namespace DeepCore.Client.Misc
{
    internal class WMessageBox
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        internal static extern IntPtr MessageBox(int hWnd, String text, String caption, uint type);
        internal static void HandleInternalFailure(string message,bool shouldclose)
        {
            SystemSounds.Asterisk.Play();
            const string errorTitle = "DeepClient - INTERNAL FAILURE!";
            IntPtr messageBoxResult = MessageBox(0, message, errorTitle, 0);
            while (shouldclose)
            {
                Process.GetCurrentProcess().Kill();
            }
        }
    }
}
