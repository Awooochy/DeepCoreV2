using DeepCore.Client.Misc;
using UnityEngine;
using UnityEngine.XR;
using VRC;
using VRC.SDKBase;

namespace DeepCore.Client.Module.Movement
{
    internal class Flight
    {
        public static float FlySpeed = 5f;
        public static bool fuck = false;
        public static bool fuck2 = false;
        public static bool IsFlyEnabled = false;
        private static Vector3 _originalGravity;
        private static Vector3 _originalVelocity;
        public static void FlyToggle()
        {
            IsFlyEnabled = !IsFlyEnabled;
            if (IsFlyEnabled)
            {
                _originalGravity = Physics.gravity;
                _originalVelocity = Player.prop_Player_0.field_Private_VRCPlayerApi_0.GetVelocity();
                Physics.gravity = Vector3.zero;
                Player.prop_Player_0.field_Private_VRCPlayerApi_0.SetVelocity(Vector3.zero);
                VrcExtensions.ToggleCharacterController(false);
                VrcExtensions.HudNotif("Flight Enabled.");
            }
            else
            {
                fuck = !fuck;
                fuck2 = !fuck2;
                Physics.gravity = _originalGravity;
                Networking.LocalPlayer.SetVelocity(_originalVelocity);
                VrcExtensions.ToggleCharacterController(true);  
                VrcExtensions.HudNotif("Flight Disabled.");
            }
        }
        public static void FlyUpdate()
        {
            if (Networking.LocalPlayer != null)
            {
                if (fuck2)
                {
                    Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = true;
                    fuck2 = false;
                }
                if (IsFlyEnabled)
                {
                    foreach (var player in VRCPlayerApi.AllPlayers)
                    {
                        if (player.isLocal)
                        {
                            if (fuck)
                            {
                                Player.prop_Player_0.gameObject.GetComponent<CharacterController>().enabled = false;
                                fuck = false;
                            }
                            var transform = player.gameObject.transform;
                            if (Input.GetKey(KeyCode.W))
                            {
                                transform.position += transform.forward * FlySpeed * Time.deltaTime;
                            }
                            if (Input.GetKey(KeyCode.S))
                            {
                                transform.position -= transform.forward * FlySpeed * Time.deltaTime;
                            }
                            if (Input.GetKey(KeyCode.A))
                            {
                                transform.position -= transform.right * FlySpeed * Time.deltaTime;
                            }
                            if (Input.GetKey(KeyCode.D))
                            {
                                transform.position += transform.right * FlySpeed * Time.deltaTime;
                            }
                            if (Input.GetKey(KeyCode.E))
                            {
                                transform.position += transform.up * FlySpeed * Time.deltaTime;
                            }
                            if (Input.GetKey(KeyCode.Q))
                            {
                                transform.position -= transform.up * FlySpeed * Time.deltaTime;
                            }
                            else
                            {
                                if (XRSettings.isDeviceActive)
                                {
                                    if (Binds.MoveJoystick.GetAxis(Valve.VR.SteamVR_Input_Sources.LeftHand).y > 0f)
                                    {
                                        transform.position += transform.forward * FlySpeed * Time.deltaTime;
                                    }
                                    if (Binds.MoveJoystick.GetAxis(Valve.VR.SteamVR_Input_Sources.LeftHand).y < -0.5f)
                                    {
                                        transform.position -= transform.forward * FlySpeed * Time.deltaTime;
                                    }
                                    if (Binds.MoveJoystick.GetAxis(Valve.VR.SteamVR_Input_Sources.LeftHand).x < 0f)
                                    {
                                        transform.position -= transform.right * FlySpeed * Time.deltaTime;
                                    }
                                    if (Binds.MoveJoystick.GetAxis(Valve.VR.SteamVR_Input_Sources.LeftHand).x > 0f)
                                    {
                                        transform.position += transform.right * FlySpeed * Time.deltaTime;
                                    }
                                    if (Binds.RotateJoystick.GetAxis(Valve.VR.SteamVR_Input_Sources.RightHand).y > 0.5f)
                                    {
                                        transform.position += transform.up * FlySpeed * Time.deltaTime;
                                    }
                                    if (Binds.RotateJoystick.GetAxis(Valve.VR.SteamVR_Input_Sources.RightHand).y < -0.5f)
                                    {
                                        transform.position -= transform.up * FlySpeed * Time.deltaTime;
                                    }
                                }
                                Networking.LocalPlayer.SetVelocity(Vector3.zero);
                            }
                        }
                    }
                }
            }
        }
    }
}