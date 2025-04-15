using DeepClient.Client.Misc;
using UnityEngine;
using VRC.SDKBase;

namespace DeepClient.Client.Module.Movement
{
    internal class Flight
    {
        public static float FlySpeed = 10f;
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
                _originalVelocity = VRCPlayer.field_Internal_Static_MonoBehaviour1PublicOb_pOb_s_pBoGaOb_pStUnique_0.field_Private_VRCPlayerApi_0.GetVelocity();
                Physics.gravity = Vector3.zero;
                VRCPlayer.field_Internal_Static_MonoBehaviour1PublicOb_pOb_s_pBoGaOb_pStUnique_0.field_Private_VRCPlayerApi_0.SetVelocity(Vector3.zero);
                VrcExtensions.ToggleCharacterController(false);
                DeepConsole.LogConsole("Flight", "Flight Enabled.");
                PopupHelper.HudNotif("Flight Enabled.");
            }
            else
            {
                fuck = !fuck;
                fuck2 = !fuck2;
                Physics.gravity = _originalGravity;
                Networking.LocalPlayer.SetVelocity(_originalVelocity);
                VrcExtensions.ToggleCharacterController(true);
                DeepConsole.LogConsole("Flight", "Flight Disabled.");
                PopupHelper.HudNotif("Flight Disabled.");
            }
        }
        public static void FlyUpdate()
        {
            if (Networking.LocalPlayer != null)
            {
                if (fuck2)
                {
                    VRCPlayer.field_Internal_Static_MonoBehaviour1PublicOb_pOb_s_pBoGaOb_pStUnique_0.gameObject.GetComponent<CharacterController>().enabled = true;
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
                                VRCPlayer.field_Internal_Static_MonoBehaviour1PublicOb_pOb_s_pBoGaOb_pStUnique_0.gameObject.GetComponent<CharacterController>().enabled = false;
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
                            if (Input.GetKey(KeyCode.Space))
                            {
                                transform.position += transform.up * FlySpeed * Time.deltaTime;
                            }
                            if (Input.GetKey(KeyCode.LeftShift))
                            {
                                transform.position -= transform.up * FlySpeed * Time.deltaTime;
                            }
                            else
                            {
                                if (Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickVertical") > 0f)
                                {
                                    transform.position += transform.forward * FlySpeed * Time.deltaTime;
                                }
                                if (Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickVertical") < -0.5f)
                                {
                                    transform.position -= transform.forward * FlySpeed * Time.deltaTime;
                                }
                                if (Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickHorizontal") < 0f)
                                {
                                    transform.position -= transform.right * FlySpeed * Time.deltaTime;
                                }
                                if (Input.GetAxis("Oculus_CrossPlatform_PrimaryThumbstickHorizontal") > 0f)
                                {
                                    transform.position += transform.right * FlySpeed * Time.deltaTime;
                                }
                                if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") > 0.5f)
                                {
                                    transform.position += transform.up * FlySpeed * Time.deltaTime;
                                }
                                if (Input.GetAxis("Oculus_CrossPlatform_SecondaryThumbstickVertical") < -0.5f)
                                {
                                    transform.position -= transform.up * FlySpeed * Time.deltaTime;
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