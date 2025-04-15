using DeepCore.Client.Misc;
using UnityEngine;

namespace DeepCore.Client.Module.QOL
{
    internal class KeyBindManager
    {
        public static float doublePressTime = 0.3f;
        private static float lastPressTime = 0f;
        public static void OnUpdate()
        {
            if (ConfManager.FlyKeyBind.Value)
            {
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))
                {
                    Movement.Flight.FlyToggle();
                }
            }
            if (ConfManager.DoubleFlyKeyBind.Value)
            {
                if (Entry.IsInVR)
                {
                    if (Binds.Button_Jump.stateDown)
                    {
                        if (Time.time - lastPressTime <= doublePressTime)
                        {
                            Movement.Flight.FlyToggle();
                        }
                        lastPressTime = Time.time;
                    }
                }
                else
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        if (Time.time - lastPressTime <= doublePressTime)
                        {
                            Movement.Flight.FlyToggle();
                        }
                        lastPressTime = Time.time;
                    }
                }
            }
            if (ConfManager.SerializeKeyBind.Value)
            {
                if (Input.GetKeyDown(KeyCode.BackQuote))
                {
                    Photon.MovementSerilize.State();
                }
            }
            if (Visual.FlipScreen.IsEnabled)
            {
                Visual.FlipScreen.OnUpdate();
            }
        }
    }
}
