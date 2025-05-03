namespace AstroClient.MenuApi.ActionMenuAPI
{
    using System;
    using Api;
    using Helpers;
    using Managers;
    using MelonLoader;
    using Types;
    using UnityEngine;

    public abstract class PedalStruct
    {
        public string text { get; set; }
        public Texture2D icon { get; set; }
        public Action<ActionMenu> triggerEvent { get; protected set; }
        public PedalType Type { get; protected set; }
        public bool locked { get; set; }
        public bool shouldAdd { get; set; } = true;
    }

    public sealed class PedalButton : PedalStruct
    {
        public PedalButton(string text, Texture2D icon, Action triggerEvent, bool locked = false)
        {
            this.text = text;
            this.icon = icon;
            this.triggerEvent = delegate { triggerEvent(); };
            Type = PedalType.Button;
            this.locked = locked;
        }
    }

    public sealed class PedalFourAxis : PedalStruct
    {
        public PedalFourAxis(string text, Texture2D icon, Action<Vector2> onUpdate, Action onOpen, Action onClose,
            string topButtonText, string rightButtonText, string downButtonText, string leftButtonText, bool locked = false)
        {
            this.text = text;
            this.icon = icon;
            triggerEvent = menu =>
            {
                try
                {
                    onOpen?.Invoke();
                }
                catch (Exception e)
                {
                    MelonLogger.Error($"Exception caught in onOpen action passed to Four Axis Puppet: {e}");
                }
                var puppet = FourAxisPuppetManager.OpenFourAxisMenu(menu.GetActionMenuType(), text, onUpdate, onClose, pedal);
                puppet.ButtonUp.SetButtonText(topButtonText);
                puppet.ButtonRight.SetButtonText(rightButtonText);
                puppet.ButtonDown.SetButtonText(downButtonText);
                puppet.ButtonLeft.SetButtonText(leftButtonText);
            };
            Type = PedalType.FourAxisPuppet;
            this.locked = locked;
        }

        public PedalOption pedal { get; set; }
    }

    public sealed class PedalRadial : PedalStruct
    {
        public float currentValue;

        public PedalRadial(string text, float startingValue, Texture2D icon, Action<float> onUpdate,
            Action onOpen = null, Action onClose = null, bool locked = false, bool restricted = false)
        {
            this.text = text;
            currentValue = startingValue;
            this.icon = icon;
            triggerEvent = delegate (ActionMenu menu)
            {
                try
                {
                    onOpen?.Invoke();
                }
                catch (Exception e)
                {
                    MelonLogger.Error($"Exception caught in onClose action passed to Radial Puppet", e);
                }
                var combinedAction = (Action<float>)Delegate.Combine(new Action<float>(delegate (float f)
                {
                    startingValue = f;
                    pedal.SetButtonPercentText($"{Math.Round(startingValue * 100)}%");
                }), onUpdate);
                // var hand = menu.field_Private_MonoBehaviourPublicCaObAc1BoSiBoObObObUnique_0.GetActionMenuType();
                RadialPuppetManager.OpenRadialMenu(menu.GetActionMenuType(), startingValue, combinedAction, onClose, text, pedal, restricted);
            };
            Type = PedalType.RadialPuppet;
            this.locked = locked;
            this.restricted = restricted;
        }

        public PedalOption pedal { get; set; }
        public bool restricted { get; }
    }

    public sealed class PedalSubMenu : PedalStruct
    {
        public PedalSubMenu(Action<CustomSubMenu> onSubMenuOpen, string text = null, Texture2D icon = null, Action onSubMenuClose = null, bool locked = false)
        {
            this.text = text;
            this.icon = icon;
            this.OnSubMenuOpen += onSubMenuOpen;
            this.OnSubMenuClose += onSubMenuClose;
            this.OnSubMenuClose += delegate
            {
                IsOpen = false;
            };
            triggerEvent = delegate (ActionMenu menu)
            {
                IsOpen = true;
                menu.PushPage(() => _openFunc(new CustomSubMenu(menu)), _closeFunc, icon, text);
            };
            Type = PedalType.SubMenu;
            this.locked = locked;
        }

        private Action<CustomSubMenu> _openFunc;
        /// <summary>
        /// Triggered when the submenu is opened *duh*
        /// </summary>
        public event Action<CustomSubMenu> OnSubMenuOpen
        {
            add
            {
                if (_openFunc is null)
                    _openFunc = value;
                else
                    _openFunc = (Action<CustomSubMenu>)Delegate.Combine(_openFunc, value);
            }
            remove
            {
                if (_openFunc is not null)
                    _openFunc = (Action<CustomSubMenu>)Delegate.Remove(_openFunc, value);
            }
        }

        private Action _closeFunc;

        /// <summary>
        /// Triggered when the sub menu is close *duh*
        /// </summary>
        public event Action OnSubMenuClose
        {
            add
            {
                if (_closeFunc is null)
                    _closeFunc = value;
                else
                    _closeFunc = (Action)Delegate.Combine(_closeFunc, value);
            }
            remove
            {
                if (_closeFunc is not null)
                    _closeFunc = (Action)Delegate.Remove(_closeFunc, value);
            }
        }

        public bool IsOpen { get; internal set; }
    }

    public sealed class PedalToggle : PedalStruct
    {
        public PedalToggle(string text, Action<bool> onToggle, bool toggled, Texture2D icon = null,
            bool locked = false)
        {
            this.text = text;
            this.toggled = toggled;
            this.icon = icon;
            triggerEvent = delegate
            {
                //MelonLogger.Msg($"Old state: {this.toggled}, New state: {!this.toggled}");
                this.toggled = !this.toggled;
                if (this.toggled)
                    pedal.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeToggleOn);
                else
                    pedal.SetPedalTypeIcon(Utilities.GetExpressionsIcons().typeToggleOff);
                onToggle.Invoke(toggled);
            };
            Type = PedalType.Toggle;
            this.locked = locked;
        }

        public bool toggled { get; set; }

        public PedalOption pedal { get; set; }
    }
}