using DeepCore.Client.Misc;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace DeepCore.Client.Module.Photon
{
    internal class MovementSerilize
    {
        public static bool IsEnabled = false;
        public static void State()
        {
            IsEnabled =! IsEnabled;
            if (IsEnabled)
            {
                DeepConsole.Log("Photon", "Serilizer is enabled.");
                VrcExtensions.HudNotif("Serilizer is enabled.");
            }
            else 
            {
                IsEnabled = false;
                DeepConsole.Log("Photon", "Serilizer is disabled.");
                VrcExtensions.HudNotif("Serilizer is disabled.");
            }
        }
        public static bool OnEventSent(byte code, object data, RaiseEventOptions options, SendOptions sendOptions)
        {
            bool flag = code == 12;
            if (flag)
            {
                bool flag2 = IsEnabled;
                if (flag2)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
