using UnityEngine;
using VRC.SDKBase;

namespace DeepCore.Client.Module.Visual
{
    internal class OptifineZoom
    {
    public static void Update()
    {
            if (ConfManager.OptifineZoom.Value)
            {
                if (Networking.LocalPlayer != null)
                {
                    if (Input.GetKey(KeyCode.LeftAlt))
                    {
                        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 10, Time.deltaTime * 5f);
                    }
                    else
                    {
                        Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, 60, Time.deltaTime * 5f);
                    }
                }
            }
        }
    }
}
