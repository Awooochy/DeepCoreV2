namespace AstroClient.MenuApi.ActionMenuAPI.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using HarmonyLib;
    using MelonLoader;
    using Types;
    using UnhollowerRuntimeLib.XrefScans;
    using UnityEngine;
    using Object = UnityEngine.Object;

    internal static class Utilities
    {
        private static RefreshAMDelegate refreshAMDelegate;

        private static RefreshAMDelegate GetRefreshAMDelegate
        {
            get
            {
                //Build 1296 menu.Method_Public_Void_PDM_2
                if (refreshAMDelegate != null) return refreshAMDelegate;
                var refreshAMMethod = typeof(ActionMenu).GetMethods().First(
                    m =>
                        m.Name.StartsWith("Method_Public_Void_PDM_")
                        && !m.HasStringLiterals()
                        && m.SameClassMethodCallCount(1)
                        && m.HasMethodCallWithName("ThrowArgumentOutOfRangeException")
                        && !m.HasMethodWithDeclaringType(typeof(ActionMenuDriver))
                );
                refreshAMDelegate = (RefreshAMDelegate)Delegate.CreateDelegate(
                    typeof(RefreshAMDelegate),
                    null,
                    refreshAMMethod);
                return refreshAMDelegate;
            }
        }

        public static void AddPedalsInList(List<PedalStruct> list, ActionMenu instance)
        {
            foreach (var pedalStruct in list)
            {
                if (!pedalStruct.shouldAdd) continue;
                var pedalOption = instance.AddOption();
                pedalOption.SetText(pedalStruct.text);
                if (!pedalStruct.locked) pedalOption.SetPedalAction(delegate { pedalStruct.triggerEvent.Invoke(instance); });
                else pedalOption.Lock();
                //Additional setup for pedals
                switch (pedalStruct.Type)
                {
                    /*case PedalType.SubMenu:
                        pedalOption.SetPedalTypeIcon(GetExpressionsIcons().typeFolder);
                        break;*/
                    case PedalType.RadialPuppet:
                        var pedalRadial = (PedalRadial)pedalStruct;
                        pedalOption.SetPedalTypeIcon(GetExpressionsIcons().typeRadial);
                        pedalOption.SetButtonPercentText($"{Math.Round(pedalRadial.currentValue)}%");
                        pedalRadial.pedal = pedalOption;
                        pedalOption.SetBackgroundIcon(pedalStruct.icon);
                        break;
                    case PedalType.Toggle:
                        var pedalToggle = (PedalToggle)pedalStruct;
                        if (pedalToggle.toggled)
                            pedalOption.SetPedalTypeIcon(GetExpressionsIcons().typeToggleOn);
                        else
                            pedalOption.SetPedalTypeIcon(GetExpressionsIcons().typeToggleOff);
                        pedalToggle.pedal = pedalOption;
                        pedalOption.SetBackgroundIcon(pedalStruct.icon);
                        break;
                    case PedalType.FourAxisPuppet:
                        pedalOption.SetPedalTypeIcon(GetExpressionsIcons().typeAxis);
                        pedalOption.SetBackgroundIcon(pedalStruct.icon);
                        break;
                    default:
                        pedalOption.SetForegroundIcon(pedalStruct.icon);
                        break;
                }
            }
        }

        public static float ConvertFromDegToEuler(float angle)
        {
            //TODO: Rewrite/Remove Unnecessary Addition/Subtraction
            if (angle >= 0 && angle <= 90) return 90 - angle;
            if (angle > 90 && angle <= 180) return 360 - (angle - 90);
            if (angle <= -90 && angle >= -180) return 270 - (angle + 180);
            if (angle <= 0 && angle >= -90) return 180 - (angle + 180) + 90;
            return 0;
        }

        public static float ConvertFromEuler(float angle)
        {
            //TODO: Rewrite/Remove Unnecessary Addition/Subtraction
            if (angle >= 90 && angle <= 270) return (angle - 90) * -1;
            if (angle <= 360 && angle > 270) return 180 - (angle - 270);
            if (angle < 90 && angle >= 0) return 90 - angle;
            return 0;
        }

        public static GameObject CloneChild(Transform parent, string pathToGameObject)
        {
            var go = parent.Find(pathToGameObject);
            return Object
                .Instantiate(go, parent)
                .gameObject;
        }

        public static ActionMenuDriver.ExpressionIcons GetExpressionsIcons()
        {
            var driver = GetDriver();
            return driver.field_Public_ExpressionIcons_0;
        }

        public static void ScanMethod(MethodInfo m)
        {
            MelonLogger.Msg($"Scanning: {m.FullDescription()}");
            foreach (var instance in XrefScanner.XrefScan(m))
                try
                {
                    if (instance.Type == XrefType.Global && instance.ReadAsObject() != null)
                        try
                        {
                            MelonLogger.Msg($"   Found String: {instance.ReadAsObject().ToString()}");
                        }
                        catch
                        {
                        }
                    else if (instance.Type == XrefType.Method && instance.TryResolve() != null)
                        try
                        {
                            MelonLogger.Msg($"   Found Method: {instance.TryResolve().FullDescription()}");
                        }
                        catch
                        {
                        }
                }
                catch
                {
                }

            foreach (var instance in XrefScanner.UsedBy(m))
                try
                {
                    if (instance.Type == XrefType.Method && instance.TryResolve() != null)
                        try
                        {
                            MelonLogger.Msg($"   Found Used By Method: {instance.TryResolve().FullDescription()}");
                        }
                        catch
                        {
                        }
                }
                catch
                {
                }
        }

        public static void RefreshAM()
        {
            var driver = GetDriver();
            if (driver == null)
            {
                Log.Warn("Refresh called before driver init");
                return;
            }

            var leftOpener = driver.GetLeftOpener();
            GetRefreshAMDelegate(leftOpener.GetActionMenu());
            var rightOpener = driver.GetRightOpener();
            GetRefreshAMDelegate(rightOpener.GetActionMenu());
        }

        public static void ResetMenu()
        {
            var driver = GetDriver();
            if (driver == null)
            {
                Log.Warn("Reset called before driver init");
                return;
            }

            var leftOpener = driver.GetLeftOpener();
            leftOpener.GetActionMenu().Reset();

            var rightOpener = driver.GetRightOpener();
            rightOpener.GetActionMenu().Reset();

        }

        public static ActionMenuDriver GetDriver()
        {
            return ActionMenuDriver.field_Public_Static_ActionMenuDriver_0;
        }

        public static (double x1, double y1, double x2, double y2) GetIntersection(float x, float y, float r)
        {
            var tmp = Math.Pow(y / x, 2);
            var c4 = -Math.Pow(r, 2) * -4;
            var x1 = Math.Sqrt(c4 + c4 * tmp) / (2 + 2 * tmp);
            var x2 = -x1;
            return (x1, x1 * (y / x), x2, x2 * (y / x));
        }

        private delegate void RefreshAMDelegate(ActionMenu actionMenu);
    }
}