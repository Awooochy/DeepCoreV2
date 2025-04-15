namespace DeepClient.Client.Module.Movement
{
    internal class ForceJump
    {
        public static float OriginalJumpImpulse { get; private set; }
        public static bool IsEnabled = false;
        public static void State(bool s)
        {
            IsEnabled = s;
            var LocalPlayer = VRCPlayer.field_Internal_Static_MonoBehaviour1PublicOb_pOb_s_pBoGaOb_pStUnique_0;
            if (IsEnabled)
            {
                if (LocalPlayer == null)
                    return;
                OriginalJumpImpulse = LocalPlayer.field_Private_VRCPlayerApi_0.GetJumpImpulse();
                LocalPlayer.field_Private_VRCPlayerApi_0.SetJumpImpulse(2.8f);
            }
            else
            {
                LocalPlayer.field_Private_VRCPlayerApi_0.SetJumpImpulse(-OriginalJumpImpulse);
            }
        }
    }
}