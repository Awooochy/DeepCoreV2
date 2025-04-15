using UnityEngine;

namespace DeepCore.Client.Module.Visual
{
    internal class FlipScreen
    {
        public static bool IsEnabled = false;
        public static Quaternion DeflautRotation;
        public static void State(bool s)
        {
            if (s)
            {
                DeflautRotation = Camera.main.gameObject.transform.rotation;
                IsEnabled = true;
            }
            else 
            {
                IsEnabled = false;
                Camera.main.gameObject.transform.rotation = DeflautRotation;
            }
        }
        public static void OnUpdate()
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
