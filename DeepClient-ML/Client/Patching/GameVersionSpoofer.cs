using System.Collections;

namespace DeepCore.Client.Patching
{
    internal class GameVersionSpoofer
    {
        public static IEnumerator Init()
        {
            while (VRCApplication.field_Private_Static_VRCApplication_0 == null)
            {
                yield return null;
            }
            VRCApplicationSetup.field_Private_Static_VRCApplicationSetup_0.field_Public_Int32_0 = 1609;
            VRCApplicationSetup.field_Private_Static_VRCApplicationSetup_0.field_Public_String_0 = "2025.1.3p3";
            VRCApplicationSetup.field_Private_Static_VRCApplicationSetup_0.field_Public_String_1 = "Release_1343";
            DeepConsole.Log("Spoofer",$"Spoofed VRChat Build {VRCApplicationSetup.field_Private_Static_VRCApplicationSetup_0.field_Public_Int32_0} | {VRCApplicationSetup.field_Private_Static_VRCApplicationSetup_0.field_Public_String_0} | {VRCApplicationSetup.field_Private_Static_VRCApplicationSetup_0.field_Public_String_1}.");
            Misc.ERPChecker.IsMyUserERping();
        }
    }
}
