using UnityEngine;
using VRC;

namespace DeepCore.Client.Module.Movement
{
    internal class TPose
    {
        public static void State()
        {
            Animator field_Internal_Animator_ = Player.prop_Player_0._vrcplayer.field_Internal_Animator_0;
            field_Internal_Animator_.enabled = !field_Internal_Animator_.enabled;
        }
    }
}
