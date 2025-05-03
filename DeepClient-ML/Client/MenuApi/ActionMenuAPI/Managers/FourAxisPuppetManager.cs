namespace AstroClient.MenuApi.ActionMenuAPI.Managers;

using System;
using ClientActions;
using Helpers;
using MelonLoader;
using UnityEngine;

internal class FourAxisPuppetManager : AstroEvents
{
    private static FourAxisPuppetImpl _leftImpl;
    private static FourAxisPuppetImpl _rightImpl;

    internal override void RegisterToEvents()
    {
        ClientEventActions.VRChat_OnActionMenuInit += VRChat_OnActionMenuInit;
        ClientEventActions.OnUpdate += OnUpdate;
    }

    public static void Setup()
    {
        var driver = Utilities.GetDriver();
        _leftImpl = new FourAxisPuppetImpl(driver.GetLeftOpener().GetActionMenu(), InputManager.Left);
        _rightImpl = new FourAxisPuppetImpl(driver.GetRightOpener().GetActionMenu(), InputManager.Right);
    }

    public static FourAxisPuppetImpl OpenFourAxisMenu(ActionMenuOpener.ActionMenuType hand, string text, Action<Vector2> update, Action close, PedalOption pedalOption)
    {
        var impl = hand switch
        {
            ActionMenuOpener.ActionMenuType.Left => _leftImpl,
            ActionMenuOpener.ActionMenuType.Right => _rightImpl,
            _ => throw new ArgumentOutOfRangeException(""),
        };

        impl.OpenFourAxisMenu(text, update, close, pedalOption);

        return impl;
    }

    private void VRChat_OnActionMenuInit()
    {
        Setup();
    }

    public static void OnUpdate()
    {
        if (_leftImpl != null) _leftImpl?.OnUpdate();
        if (_rightImpl != null) _rightImpl?.OnUpdate();
    }

    public static void CloseFourAxisMenu()
    {
        if (_leftImpl != null) _leftImpl?.CloseFourAxisMenu();
        if (_rightImpl != null) _rightImpl?.CloseFourAxisMenu();

    }


    public class FourAxisPuppetImpl
    {
        private readonly ActionMenu _actionMenu;
        private readonly AxisPuppetMenu _current;
        private readonly PedalGraphic _fillUp;
        private readonly PedalGraphic _fillDown;
        private readonly PedalGraphic _fillLeft;
        private readonly PedalGraphic _fillRight;
        private readonly GameObject _cursor;
        public ActionButton ButtonUp { get; }
        public ActionButton ButtonDown { get; }
        public ActionButton ButtonLeft { get; }
        public ActionButton ButtonRight { get; }
        private readonly InputManager _input;

        public static Vector2 fourAxisPuppetValue { get; set; }

        public Action<Vector2> UpdateAction { get; set; }
        public Action CloseAction { get; set; }

        public FourAxisPuppetImpl(ActionMenu actionMenu, InputManager input)
        {
            _actionMenu = actionMenu;
            _current = Utilities.CloneChild(_actionMenu.transform, "AxisPuppetMenu").GetComponent<AxisPuppetMenu>();
            _input = input;

            ButtonUp = _current.GetButtonUp();
            ButtonDown = _current.GetButtonDown();
            ButtonLeft = _current.GetButtonLeft();
            ButtonRight = _current.GetButtonRight();
            _fillUp = _current.GetFillUp();
            _fillDown = _current.GetFillDown();
            _fillLeft = _current.GetFillLeft();
            _fillRight = _current.GetFillRight();
            _cursor = _current.GetCursor();
        }

        public void OnUpdate()
        {
            if (_current == null || _input == null)
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
                CloseFourAxisMenu();
                return;
            }

            fourAxisPuppetValue = _input.GetStick() / 16;
            var x = fourAxisPuppetValue.x;
            var y = fourAxisPuppetValue.y;

            // Paint background alpha
            var (alphaLeft, alphaRight) = (x >= 0) switch {
                true => (0.0f, x),
                false => (Math.Abs(x), 0.0f),
            };
            _fillLeft.SetAlpha(alphaLeft);
            _fillRight.SetAlpha(alphaRight);

            var (alphaDown, alphaUp) = (y >= 0) switch {
                true => (0.0f, y),
                false => (Math.Abs(y), 0.0f),
            };
            _fillDown.SetAlpha(alphaDown);
            _fillUp.SetAlpha(alphaUp);

            UpdateMathStuff();
            CallUpdateAction();
        }

        public void OpenFourAxisMenu(string title, Action<Vector2> update, Action close, PedalOption pedalOption)
        {
            if (_current.gameObject.active) return;

            Input.ResetInputAxes();
            InputManager.ResetMousePos();
            UpdateAction = update;
            CloseAction = close;
            _current.gameObject.SetActive(true);
            _current.GetTitle().text = title;
            _actionMenu.DisableInput();
            _actionMenu.SetMainMenuOpacity(0.5f);
            _current.transform.localPosition = pedalOption.GetActionButton().transform.localPosition;
        }

        private void CallUpdateAction()
        {
            try
            {
                UpdateAction?.Invoke(fourAxisPuppetValue);
            }
            catch (Exception e)
            {
                MelonLogger.Error($"Exception caught in onUpdate action passed to Four Axis Puppet: {e}");
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
                MelonLogger.Error($"Exception caught in onClose action passed to Radial Puppet", e);
            }
        }

        public void CloseFourAxisMenu()
        {
            if (!_current.isActiveAndEnabled) return;
            CallUpdateAction();
            CallCloseAction();
            _current.gameObject.SetActive(false);
            _actionMenu.SetMainMenuOpacity();
            _actionMenu.EnableInput();
        }

        private void UpdateMathStuff()
        {
            _cursor.transform.localPosition = _input.GetStick() * 4;
        }
    }

}
