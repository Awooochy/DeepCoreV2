using System;
using System.Media;
using DeepCore.Client.Misc;
using UnhollowerBaseLib;
using VRC;
using VRC.SDKBase;

namespace DeepCore.Client.Module.RPC
{
    internal class RPCManager
    {
        public static void HandleRPC(Player param_1, VRC_EventHandler.VrcEvent param_2, VRC_EventHandler.VrcBroadcastType param_3)
        {
            if (param_2.ParameterString.StartsWith("DCChat:"))
            {
                var playerApi = param_1.field_Private_VRCPlayerApi_0;
                string extractedValue = param_2.ParameterString.Substring(7);
                ClientMenu.Pages_MainMenu.ClientChat.ChatMSG(playerApi.displayName,extractedValue);
            }
            if (param_2.ParameterString == "DCWarn:")
            {
                string extractedValue = param_2.ParameterString.Substring(7);
                PopupHelper.OpenVideoInMM("DeepClient - VideoPlayer", extractedValue, true);
            }
        }
        public static void SendRPC(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return;
            }
            string extractedValue = input.Trim();
            if (string.IsNullOrEmpty(extractedValue))
            {
                VrcExtensions.AlertPopup("RPC | ALERT", "You can't send nothing!", 10);
                return;
            }
            Networking.RPC(0, OnLoadedScaneManager.DeepCoreRpcObject, extractedValue, new Il2CppReferenceArray<Il2CppSystem.Object>(0L));
        }
    }
}
