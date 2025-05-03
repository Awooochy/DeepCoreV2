namespace AstroClient.MenuApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using VRC.Localization;
    using xAstroBoy.Extensions;
    using static VRC.Localization.LocalizableStringExtensions;


    internal static class ApiToolsExts
    {
        internal static LocalizableString Localize(this string str)
        {
            return VRC.Localization.LocalizableStringExtensions.Localize(str);
        }

        internal static string ToString(this LocalizableString str)
        {
            if (str == null)
            {
                return string.Empty;
            }

            if (str._localizationKey.IsNullOrEmptyOrWhiteSpace())
            {
                return str._fallbackText;
            }

            return str._localizationKey;
        }

    }
}
