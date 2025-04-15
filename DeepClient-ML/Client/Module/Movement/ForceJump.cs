using VRC;

namespace DeepCore.Client.Module.Movement
{
    internal class ForceJump
    {
        public static float OriginalJumpImpulse { get; private set; }
        public static void State(bool s)
        {
            var LocalPlayer = Player.prop_Player_0;
            if (s)
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
