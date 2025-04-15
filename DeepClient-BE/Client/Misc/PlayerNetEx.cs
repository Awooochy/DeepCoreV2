using UnityEngine;

namespace DeepClient.Client.Misc
{
    public static class PlayerNetEx
    {
        public static string GetFramesColord(this PlayerNet_Internal player)
        {
            float frames = player.GetFramerate();
            string FrameText;
            if (frames > 60f)
                FrameText = "<color=green>" + frames.ToString() + "</color>";
            else
            {
                if (frames > 25f)
                    FrameText = "<color=yellow>" + frames.ToString() + "</color>";
                else
                    FrameText = "<color=red>" + frames.ToString() + "</color>";
            }
            return FrameText;
        }
        public static int GetPing(this PlayerNet_Internal playerNet)
        {
            if (playerNet == null)
                return 0;
            return playerNet.prop_Int16_0;
        }
        public static string GetPingColord(this PlayerNet_Internal player)
        {
            short ping = (short)player.GetPing();
            string PingText;
            if (ping > 200)
                PingText = "<color=red>" + ping.ToString() + "</color>";
            else
            {
                if (ping > 80)
                    PingText = "<color=yellow>" + ping.ToString() + "</color>";
                else
                    PingText = "<color=green>" + ping.ToString() + "</color>";
            }
            return PingText;
        }
        public static bool ClientDetect(this PlayerNet_Internal player)
        {
            return player.GetFramerate() > 250f || player.GetFramerate() < 1f || player.GetPing() > 650 || player.GetPing() < 1;
        }
    }
}