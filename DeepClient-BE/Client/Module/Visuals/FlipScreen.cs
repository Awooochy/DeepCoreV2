using UnityEngine;

namespace DeepClient.Client.Module.Visuals
{
    internal class FlipScreen
    {
        public static bool IsEnabled = false;
        public static void OnUpdate()
        {
            {
                if (!Input.GetKey(KeyCode.LeftControl)) return;

                Camera camera = Camera.main;
                if (camera == null) return;

                float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
                if (mouseWheel > 0.1)
                {
                    Quaternion quaternion = camera.transform.rotation;
                    Vector3 euler = quaternion.eulerAngles;
                    euler -= Vector3.forward;
                    camera.transform.rotation = Quaternion.Euler(euler);
                }
                else if (mouseWheel < -0.1)
                {
                    Quaternion quaternion = camera.transform.rotation;
                    Vector3 euler = quaternion.eulerAngles;
                    euler += Vector3.forward;
                    camera.transform.rotation = Quaternion.Euler(euler);
                }
            }
        }
    }
}
