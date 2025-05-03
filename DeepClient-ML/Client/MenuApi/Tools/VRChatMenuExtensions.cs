namespace AstroClient.MenuApi.Tools
{
    using VRC.Localization;

    internal static class VRChatMenuExtensions
    {


        internal static LocalizableString Localize(this string str)
        {
            return LocalizableStringExtensions.Localize(str);
        }


    }
}
