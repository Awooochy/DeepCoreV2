using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeepCore.Client.Misc;
using ExitGames.Client.Photon;
using Photon.Realtime;

namespace DeepCore.Client.Module.Photon
{
    internal class PhtonManagerUtils
    {
        public static bool isdebugtime = false;
        public static bool PhotonEvent(EventData param_1)
        {
            if (isdebugtime)
            {
                DeepConsole.LogConsole("Photon", "--------------------------------------------------");
                DeepConsole.LogConsole("Photon", $"Event Code:{param_1.Code}");
                DeepConsole.LogConsole("Photon", $"Event type:{param_1.Sender}");
                DeepConsole.LogConsole("Photon", $"Sender:{param_1.Sender}");
                DeepConsole.LogConsole("Photon", $"SenderKey:{param_1.SenderKey}");
                DeepConsole.LogConsole("Photon", $"Parameters:{param_1.Parameters}");
                DeepConsole.LogConsole("Photon", $"Pointer:{param_1.Pointer}");
                DeepConsole.LogConsole("Photon", $"Data:{PrintByteArray(SerializationUtil.ToByteArray(param_1.CustomData))}");
                DeepConsole.LogConsole("Photon", "--------------------------------------------------");
            }
            if (param_1.Code == 33)
            {
                return ModerationNotice.OnEventPatch(param_1);
            }
            if (ChatBoxLogger.OnEvent(param_1))
            {
                return true;
            }
            return true;
        }
        public static string PrintByteArray(byte[] bytes)
        {
            var sb = new StringBuilder("[");
            foreach (var b in bytes)
            {
                sb.Append(b + ", ");
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
