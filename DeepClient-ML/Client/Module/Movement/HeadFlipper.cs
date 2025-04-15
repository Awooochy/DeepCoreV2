using VRC;
using VRC.DataModel;

namespace DeepCore.Client.Module.Movement
{
    internal class HeadFlipper
    {
        private static NeckRange _nexkRange;
        public static void state(bool s)
        {
            Player player = Player.prop_Player_0;
            if (s)
            {
                _nexkRange = player.GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0;
                player.GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0 = new NeckRange(float.MinValue, float.MaxValue, 0f);
            }
            else
            {
                player.GetComponent<GamelikeInputController>().field_Protected_NeckMouseRotator_0.field_Public_NeckRange_0 = _nexkRange;
            }
        }
    }
}
