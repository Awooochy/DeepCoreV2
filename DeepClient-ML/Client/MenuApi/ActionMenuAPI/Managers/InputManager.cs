namespace AstroClient.MenuApi.ActionMenuAPI.Managers;

using Helpers;
using Il2CppSystem.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using static Helpers.Constants;

internal class InputManager
{
    public static InputManager Left { get; } = new InputManager(LEFT_TRIGGER, LEFT_HORIZONTAL, LEFT_VERTICAL);
    public static InputManager Right { get; } = new InputManager(RIGHT_TRIGGER, RIGHT_HORIZONTAL, RIGHT_VERTICAL);

    private readonly string _inputTrigger;
    private readonly string _inputHorizontal;
    private readonly string _inputVertical;
    public InputManager(string trigger, string horizontal, string vertical)
    {
        _inputTrigger = trigger;
        _inputHorizontal = horizontal;
        _inputVertical = vertical;
    }

    private Vector2 ControllerInput
        => new Vector2(Input.GetAxis(_inputHorizontal), Input.GetAxis(_inputVertical)) * 16;

    public Vector2 GetStick()
    {
        return IsXrPresent ?
            ControllerInput :
            MouseInput;
    }

    public bool GetClicked()
    {
        return IsXrPresent ?
            Input.GetAxis(_inputTrigger) >= 0.4f : 
            Input.GetMouseButtonUp(0);
    }

    private static Vector2 mouseAxis;

    private static Vector2 MouseInput
    {
        get
        {
            mouseAxis.x = Mathf.Clamp(mouseAxis.x + Input.GetAxis("Mouse X"), -16f, 16f);
            mouseAxis.y = Mathf.Clamp(mouseAxis.y + Input.GetAxis("Mouse Y"), -16f, 16f);
            var (x1, y1, x2, y2) = Utilities.GetIntersection(mouseAxis.x, mouseAxis.y, Mathf.Max(Mathf.Abs(mouseAxis.x), Mathf.Abs(mouseAxis.y)));
            if (x1 > 0 && mouseAxis.x > 0)
                return new Vector2((float)x1, (float)y1);
            return new Vector2((float)x2, (float)y2); ;
        }
    }

    public static void ResetMousePos()
    {
        mouseAxis.x = 0f;
        mouseAxis.y = 0f;
    }

    public static bool? _isXrPresent = null;

    public static bool IsXrPresent
    {
        get
        {
            if (_isXrPresent is bool present)
            {
                return present;
            }

            var xrDisplaySubsystems = new List<XRDisplaySubsystem>();
            SubsystemManager.GetInstances(xrDisplaySubsystems);
            foreach (var xrDisplay in xrDisplaySubsystems)
            {
                if (xrDisplay.running)
                {
                    return (_isXrPresent = true).Value;
                }
            }
            return (_isXrPresent = false).Value;
        }
    }
}
