using UnityEngine;

namespace DeepClient.Client.Module.ApplicationBot
{
    internal class ObjectExtensions
    {
        public static GameObject GetPlayerCamera
        {
            get
            {
                bool flag = CachedPlayerCamera == null;
                if (flag)
                {
                    CachedPlayerCamera = GameObject.Find("Camera (eye)");
                }
                return CachedPlayerCamera;
            }
        }
        private static GameObject CachedPlayerCamera;
    }
}