namespace AstroClient.MenuApi.ActionMenuAPI.Api
{
    using System;
    using HarmonyLib;
    using MelonLoader;
    using Types;
    using UnityEngine;

    /// <summary>
    /// VRC Page specific things
    /// </summary>
    public static class VRCActionMenuPage
    {
        /// <summary>
        ///     Add a button pedal to a specific ActionMenu page
        /// </summary>
        /// <param name="pageType">The page to add the button to</param>
        /// <param name="text">Button text</param>
        /// <param name="triggerEvent">Button click action</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="locked">(optional)The starting state for the lockable pedal, true = locked, false = unlocked</param>
        /// <param name="insertion">
        ///     (optional) Determines whether or not the button is added before or after VRChat's buttons for
        ///     the target page
        /// </param>
        public static PedalButton AddButton(ActionMenuPage pageType, string text, Action triggerEvent,
            Texture2D icon = null, bool locked = false, Insertion insertion = Insertion.Post)
        {
            var pedal = new PedalButton(text, icon, triggerEvent, locked);
            AddPedalToList(pageType, pedal, insertion);
            return pedal;
        }

        /// <summary>
        ///     Add a radial puppet button pedal to a specific ActionMenu page
        /// </summary>
        /// <param name="pageType">The page to add the button to</param>
        /// <param name="text">Button text</param>
        /// <param name="onUpdate">Calls action with a float between 0 - 1 depending on the current value of the radial puppet</param>
        /// <param name="startingValue">(optional) Starting value for radial puppet 0-1</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="locked">(optional)The starting state for the lockable pedal, true = locked, false = unlocked</param>
        /// <param name="insertion">
        ///     (optional) Determines whether or not the button is added before or after VRChat's buttons for
        ///     the target page
        /// </param>
        public static PedalRadial AddRadialPuppet(ActionMenuPage pageType, string text, Action<float> onUpdate,
            Action onOpen, Action onClose, float startingValue = 0, Texture2D icon = null, bool locked = false, Insertion insertion = Insertion.Post)
        {
            var pedal = new PedalRadial(text, startingValue, icon, onUpdate, onOpen, onClose, locked);
            AddPedalToList(pageType, pedal, insertion);
            return pedal;
        }

        /// <summary>
        ///     Add a restricted radial puppet button pedal to a specific ActionMenu page. Restricted meaning that you can't rotate
        ///     past 100 to get to 0 and vice versa
        /// </summary>
        /// <param name="pageType">The page to add the button to</param>
        /// <param name="text">Button text</param>
        /// <param name="onUpdate">Calls action with a float between 0 - 1 depending on the current value of the radial puppet</param>
        /// <param name="startingValue">(optional) Starting value for radial puppet 0-1</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="locked">(optional)The starting state for the lockable pedal, true = locked, false = unlocked</param>
        /// <param name="insertion">
        ///     (optional) Determines whether or not the button is added before or after VRChat's buttons for
        ///     the target page
        /// </param>
        public static PedalRadial AddRestrictedRadialPuppet(ActionMenuPage pageType, string text,
            Action<float> onUpdate, Action onOpen, Action onClose, float startingValue = 0, Texture2D icon = null, bool locked = false,
            Insertion insertion = Insertion.Post)
        {
            var pedal = new PedalRadial(text, startingValue, icon, onUpdate, onOpen, onClose, locked, true);
            AddPedalToList(pageType, pedal, insertion);
            return pedal;
        }

        /// <summary>
        ///     Add a four axis puppet button pedal to a specific ActionMenu page
        /// </summary>
        /// <param name="pageType">The page to add the button to</param>
        /// <param name="text">Button text</param>
        /// <param name="onUpdate">
        ///     Calls action with a Vector2 with x and y being between -1 and 1 depending on the current value
        ///     of the four axis puppet
        /// </param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="insertion">
        ///     (optional) Determines whether or not the button is added before or after VRChat's buttons for
        ///     the target page
        /// </param>
        /// <param name="locked">(optional)The starting state for the lockable pedal, true = locked, false = unlocked</param>
        /// <param name="topButtonText">(optional) Top Button Button text On Four Axis Puppet</param>
        /// <param name="rightButtonText">(optional) Right Button Button text On Four Axis Puppet</param>
        /// <param name="downButtonText">(optional) Bottom Button Button text On Four Axis Puppet</param>
        /// <param name="leftButtonText">(optional) Left Button Button text On Four Axis Puppet</param>
        public static PedalFourAxis AddFourAxisPuppet(ActionMenuPage pageType, string text, Action<Vector2> onUpdate,
            Action onOpen, Action onClose = null, Texture2D icon = null, bool locked = false, string topButtonText = "Up",
            string rightButtonText = "Right", string downButtonText = "Down", string leftButtonText = "Left",
            Insertion insertion = Insertion.Post)
        {
            var pedal = new PedalFourAxis(text, icon, onUpdate, onOpen, onClose, topButtonText, rightButtonText, downButtonText,
                leftButtonText, locked);
            AddPedalToList(pageType, pedal, insertion);
            return pedal;
        }

        /// <summary>
        ///     Add a submenu to an ActionMenu page
        /// </summary>
        /// <param name="pageType">The page to add the button to</param>
        /// <param name="text">Button text</param>
        /// <param name="openFunc">
        ///     Function called when page opened. Add your methods calls to other AMAPI methods such
        ///     AddRadialPedalToSubMenu to add buttons to the submenu it creates when clicked
        /// </param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="locked">(optional)The starting state for the lockable pedal, true = locked, false = unlocked</param>
        /// <param name="closeFunc">(optional) Function called when page closes</param>
        /// <param name="insertion">
        ///     (optional) Determines whether or not the button is added before or after VRChat's buttons for
        ///     the target page
        /// </param>
        public static PedalSubMenu AddSubMenu(ActionMenuPage pageType, string text, Action<CustomSubMenu> openFunc,
            Texture2D icon = null, bool locked = false, Action closeFunc = null, Insertion insertion = Insertion.Post)
        {
            var pedal = new PedalSubMenu(openFunc, text, icon, closeFunc, locked);
            AddPedalToList(pageType, pedal, insertion);
            return pedal;
        }

        /// <summary>
        ///     Add a toggle button pedal to an ActionMenu page
        /// </summary>
        /// <param name="pageType">The page to add the button to</param>
        /// <param name="text">Button text</param>
        /// <param name="startingState">Starting value of the toggle pedal everytime the pedal is created</param>
        /// <param name="onToggle">Calls action with a bool depending on the current value of the toggle</param>
        /// <param name="icon">(optional) The Button Icon</param>
        /// <param name="locked">(optional)The starting state for the lockable pedal, true = locked, false = unlocked</param>
        /// <param name="insertion">
        ///     (optional) Determines whether or not the button is added before or after VRChat's buttons for
        ///     the target page
        /// </param>
        public static PedalToggle AddToggle(ActionMenuPage pageType, string text, bool startingState,
            Action<bool> onToggle, Texture2D icon = null, bool locked = false, Insertion insertion = Insertion.Post)
        {
            var pedal = new PedalToggle(text, onToggle, startingState, icon, locked);
            AddPedalToList(pageType, pedal, insertion);
            return pedal;
        }

        internal static void AddPedalToList(ActionMenuPage pageType, PedalStruct customPedal, Insertion insertion)
        {
            Log.Debug(
                $"Adding to page: {pageType.ToString()}, Text: {customPedal.text}, Locked: {customPedal.locked}");
            switch (pageType)
            {
                case ActionMenuPage.SDK2Expression:
                    if (insertion == Insertion.Pre) AstroActionMenuPatches.sdk2ExpressionPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) AstroActionMenuPatches.sdk2ExpressionPagePost.Add(customPedal);
                    return;
                case ActionMenuPage.Config:
                    if (insertion == Insertion.Pre) AstroActionMenuPatches.configPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) AstroActionMenuPatches.configPagePost.Add(customPedal);
                    return;
                case ActionMenuPage.Emojis:
                    if (insertion == Insertion.Pre) AstroActionMenuPatches.emojisPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) AstroActionMenuPatches.emojisPagePost.Add(customPedal);
                    return;
                case ActionMenuPage.Expression:
                    if (insertion == Insertion.Pre) AstroActionMenuPatches.expressionPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) AstroActionMenuPatches.expressionPagePost.Add(customPedal);
                    return;
                case ActionMenuPage.Main:
                    if (insertion == Insertion.Pre) AstroActionMenuPatches.mainPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) AstroActionMenuPatches.mainPagePost.Add(customPedal);
                    return;
                case ActionMenuPage.MenuOpacity:
                    Log.Warn("Submenu \"Opacity\" doesn't exist anymore");
                    goto case ActionMenuPage.Config;
                case ActionMenuPage.MenuSize:
                    Log.Warn("Submenu \"MenuSize\" doesn't exist anymore");
                    goto case ActionMenuPage.Config;
                case ActionMenuPage.Nameplates:
                    if (insertion == Insertion.Pre) AstroActionMenuPatches.nameplatesPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) AstroActionMenuPatches.nameplatesPagePost.Add(customPedal);
                    return;
                case ActionMenuPage.NameplatesOpacity:
                    Log.Warn("Submenu \"NameplatesOpacity\" doesn't exist anymore");
                    goto case ActionMenuPage.Nameplates;
                //case ActionMenuPage.NameplatesSize:
                //    if (insertion == Insertion.Pre) AstroActionMenuPatches.nameplatesSizePagePre.Add(customPedal);
                //    else if (insertion == Insertion.Post) AstroActionMenuPatches.nameplatesSizePagePost.Add(customPedal);
                //    return;
                //case ActionMenuPage.NameplatesVisibilty:
                //    if (insertion == Insertion.Pre) AstroActionMenuPatches.nameplatesVisibilityPagePre.Add(customPedal);
                //    else if (insertion == Insertion.Post) AstroActionMenuPatches.nameplatesVisibilityPagePost.Add(customPedal);
                //    return;
                case ActionMenuPage.Options:
                    if (insertion == Insertion.Pre) AstroActionMenuPatches.optionsPagePre.Add(customPedal);
                    else if (insertion == Insertion.Post) AstroActionMenuPatches.optionsPagePost.Add(customPedal);
                    return;
            }
        }
    }
}