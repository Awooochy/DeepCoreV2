using Valve.VR;

namespace DeepCore.Client.Misc
{
    internal class Binds
    {
        public static SteamVR_Action_Boolean Button_Jump;
        public static SteamVR_Action_Boolean Button_QM;
        public static SteamVR_Action_Boolean Button_Mic;
        public static SteamVR_Action_Boolean Button_Grab;
        public static SteamVR_Action_Boolean Button_Interact;
        public static SteamVR_Action_Single Trigger;
        public static SteamVR_Action_Vector2 MoveJoystick;
        public static SteamVR_Action_Vector2 RotateJoystick;

        public static void Register()
        {
            Button_Jump = SteamVR_Input.GetBooleanAction("jump", false);
            Button_Mic = SteamVR_Input.GetBooleanAction("Toggle Microphone", false);
            Button_QM = SteamVR_Input.GetBooleanAction("Menu", false);
            Button_Grab = SteamVR_Input.GetBooleanAction("Grab", false);
            Button_Interact = SteamVR_Input.GetBooleanAction("Interact", false);
            Trigger = SteamVR_Input.GetSingleAction("gesture_trigger_axis", false);
            MoveJoystick = SteamVR_Input.GetVector2Action("Move", false);
            RotateJoystick = SteamVR_Input.GetVector2Action("Rotate", false);
            DeepConsole.Log("VRBinds", "Binds Registered.");
        }
    }
}
