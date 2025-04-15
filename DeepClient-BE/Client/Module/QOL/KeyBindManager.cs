using UnityEngine;

namespace DeepClient.Client.Module.QOL
{
    internal class KeyBindManager
    {
        public static void OnUpdate()
        {
            if (ConfManager.FlyKeyBind.Value)
            {
                if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.F))
                {
                    Movement.Flight.FlyToggle();
                }
            }
            if (Visuals.FlipScreen.IsEnabled)
            {
                Visuals.FlipScreen.OnUpdate();
            }
        }
    }
}