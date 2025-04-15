using ExitGames.Client.Photon;
using Photon.Realtime;

namespace DeepCore.Client.Module.Photon
{
    internal class PhotonDebugger
    {
        public static bool IsOnEventSendDebug = false;
        public static bool OnEventSent(byte code, object data, RaiseEventOptions options, SendOptions sendOptions)
        {
            DeepConsole.LogConsole("Photon:OnEventSent",$"----------------------");
            DeepConsole.LogConsole("Photon:OnEventSent",$"Code:{code}");
            DeepConsole.LogConsole("Photon:OnEventSent",$"Data:{data}");
            DeepConsole.LogConsole("Photon:OnEventSent",$"Data:{data}");
            DeepConsole.LogConsole("Photon:OnEventSent",$"RaiseEventOptions:{options}");
            DeepConsole.LogConsole("Photon:OnEventSent",$"SendOptions:{sendOptions}");
            DeepConsole.LogConsole("Photon:OnEventSent", $"----------------------");
            return true;
        }
    }
}
