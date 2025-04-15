using ExitGames.Client.Photon;

namespace DeepClient.Client.Misc
{
    internal class PhotonEx
    {
        public static void SendChatBoxMessage(string message)
        {
            RaiseEvent(43, message, new RaiseEventOptions_Internal
            {
                field_Public_EnumPublicSealedvaDoMeReAdReSlAd13SlUnique_0 = EnumPublicSealedvaDoMeReAdReSlAd13SlUnique.DoNotCache,
                field_Public_EnumPublicSealedvaOtAlMa4vUnique_0 = EnumPublicSealedvaOtAlMa4vUnique.Others
            }, default(SendOptions));
        }
        public static bool RaiseEvent(byte eventCode, System.Object eventContent, RaiseEventOptions_Internal raiseEventOptions, SendOptions sendOptions)
        {
            Il2CppSystem.Object content = SerializationUtils.FromManagedToIL2CPP<Il2CppSystem.Object>(eventContent);
            return PhotonNetwork_Internal.Method_Public_Static_Boolean_Byte_Object_ObjectPublicObByObInByObObUnique_SendOptions_0(eventCode, content, raiseEventOptions, sendOptions);
        }
        public static bool RaiseEvent(byte eventCode, Il2CppSystem.Object eventContent, RaiseEventOptions_Internal raiseEventOptions, SendOptions sendOptions)
        {
            return PhotonNetwork_Internal.Method_Public_Static_Boolean_Byte_Object_ObjectPublicObByObInByObObUnique_SendOptions_0(eventCode, eventContent, raiseEventOptions, sendOptions);
        }
    }
}
