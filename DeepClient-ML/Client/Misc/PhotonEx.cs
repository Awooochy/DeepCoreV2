using ExitGames.Client.Photon;
using Photon.Realtime;

namespace DeepCore.Client.Misc
{
    internal class PhotonEx
    {
        public static void SendChatBoxMessage(string message)
        {
            PhotonUtil.OpRaiseEvent(43, message, new RaiseEventOptions
            {
                field_Public_EventCaching_0 = 0,
                field_Public_ReceiverGroup_0 = 0
            }, default(SendOptions));
        }
    }
}
