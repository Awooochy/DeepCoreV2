namespace AstroClient.MenuApi.ActionMenuAPI.Managers;

using System;
using ClientActions;
using Helpers;
using MelonLoader;
using UnityEngine;
using xAstroBoy;

internal class RadialPuppetManager : AstroEvents
{
    private static RadialPuppetImpl _leftImpl;
    private static RadialPuppetImpl _rightImpl;

    internal override void RegisterToEvents()
    {
        ClientEventActions.VRChat_OnActionMenuInit += VRChat_OnActionMenuInit;
        ClientEventActions.OnUpdate += OnUpdate;
    }

    private static void VRChat_OnActionMenuInit()
    {
        Setup();
    }

    public static void Setup()
    {
        var driver = Utilities.GetDriver();
        _leftImpl = new RadialPuppetImpl(driver.GetLeftOpener().GetActionMenu(), InputManager.Left);
        _rightImpl = new RadialPuppetImpl(driver.GetRightOpener().GetActionMenu(), InputManager.Right);
    }

    public static void OpenRadialMenu(ActionMenuOpener.ActionMenuType hand, float startingValue, Action<float> onUpdate, Action onClose, string title, PedalOption pedalOption, bool restricted = false)
    {
        var impl = hand switch
        {
            ActionMenuOpener.ActionMenuType.Left => _leftImpl,
            ActionMenuOpener.ActionMenuType.Right => _rightImpl,
            _ => throw new ArgumentOutOfRangeException(""),
        };

        impl.OpenRadialMenu(startingValue, onUpdate, onClose, title, pedalOption, restricted);
    }

    public static void OnUpdate()
    {
        if (_leftImpl != null) _leftImpl?.OnUpdate();
        if (_rightImpl != null) _rightImpl?.OnUpdate();
    }

    public static void CloseRadialMenu()
    {
        if (_leftImpl != null) _leftImpl?.CloseRadialMenu();
        if (_rightImpl != null) _rightImpl?.CloseRadialMenu();

    }

    private class RadialPuppetImpl
    {
        private readonly ActionMenu _actionMenu;
        private readonly RadialPuppetMenu _current;
        private readonly InputManager _input;

        private bool restricted;
        private float currentValue;

        private Action<float> UpdateAction { get; set; }
        private Action CloseAction { get; set; }

        public RadialPuppetImpl(ActionMenu actionMenu, InputManager input)
        {
            _actionMenu = actionMenu;
            _current = Utilities.CloneChild(_actionMenu.transform, "RadialPuppetMenu").GetComponent<RadialPuppetMenu>();
            _input = input;

            // Note: Might want to look into how these are supposed to be used
            _current.transform.FindObject("Container/Limits").gameObject.SetActive(false);
            _current.transform.FindObject("Container/Marker").gameObject.SetActive(false);
        }

        public void OnUpdate()
        {
            if (_current == null)
            {
                return;
            }
            if(_input == null)
            {
                return;
            }
            //Probably a better more efficient way to do all this
            if (!_current.isActiveAndEnabled)
            {
                return;
            }

            if (_input.GetClicked())
            {
                CloseRadialMenu();
                return;
            }

            UpdateMathStuff();
            CallUpdateAction();
        }

        public void OpenRadialMenu(float startingValue, Action<float> onUpdate, Action onClose, string title, PedalOption pedalOption, bool restricted = false)
        {
            if (_current.isActiveAndEnabled) return;

            this.restricted = restricted;
            Input.ResetInputAxes();
            InputManager.ResetMousePos();
            _current.gameObject.SetActive(true);
            _current.GetFill().SetFillAngle(startingValue * 360); //Please dont break
            UpdateAction = onUpdate;
            CloseAction = onClose;
            currentValue = startingValue;

            _current.GetTitle().text = title;
            _current.GetCenterText().text = $"{Mathf.Round(startingValue * 100f)}%";
            _current.GetFill().UpdateGeometry();
            _current.transform.localPosition = pedalOption.GetActionButton().transform.localPosition; //new Vector3(-256f, 0, 0); 
            var angleOriginal = Utilities.ConvertFromEuler(startingValue * 360);
            var eulerAngle = Utilities.ConvertFromDegToEuler(angleOriginal);
            _actionMenu.DisableInput();
            _actionMenu.SetMainMenuOpacity(0.5f);
            _current.UpdateArrow(angleOriginal, eulerAngle);
        }

        public void CloseRadialMenu()
        {
            if (!_current.isActiveAndEnabled) return;
            CallUpdateAction();
            CallCloseAction();
            _current.gameObject.SetActive(false);
            _actionMenu.EnableInput();
            _actionMenu.SetMainMenuOpacity();
        }

        private void CallUpdateAction()
        {
            try
            {
                UpdateAction?.Invoke(_current.GetFill().GetFillAngle() / 360f);
            }
            catch (Exception e)
            {
                Log.Error($"Exception caught in onUpdate action passed to Radial Puppet");
                Log.Exception(e);
            }
        }

        private void CallCloseAction()
        {
            try
            {
                CloseAction?.Invoke();
            }
            catch (Exception e)
            {
                Log.Error($"Exception caught in onClose action passed to Radial Puppet");
                Log.Exception(e);
            }
        }

        private void UpdateMathStuff()
        {
            var mousePos = _input.GetStick();
            _current.GetCursor().transform.localPosition = mousePos * 4;

            if (Vector2.Distance(mousePos, Vector2.zero) > 12)
            {
                var angleOriginal = Mathf.Round(Mathf.Atan2(mousePos.y, mousePos.x) * Constants.RAD_TO_DEG);
                var eulerAngle = Utilities.ConvertFromDegToEuler(angleOriginal);
                var normalisedAngle = eulerAngle / 360f;
                if (Math.Abs(normalisedAngle - currentValue) < 0.0001f) return;
                if (!restricted)
                {
                    _current.SetAngle(eulerAngle);
                    _current.UpdateArrow(angleOriginal, eulerAngle);
                }
                else
                {
                    var (euler, original, normalized) = (
                            currentValue > normalisedAngle,
                            currentValue - normalisedAngle < 0.5f,
                            normalisedAngle - currentValue < 0.5f) switch
                    {
                        (true, true, _) or (false, _, true) => (eulerAngle, angleOriginal, normalisedAngle),
                        (true, false, _) => (360, 90, 1),
                        (false, _, false) => (0, 90, 0),
                    };
                    _current.SetAngle(euler);
                    _current.UpdateArrow(original, euler);
                    currentValue = normalized;
                }
            }
        }
    }

}
