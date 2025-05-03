namespace AstroClient.MenuApi
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using AstroClient;
    using AstroClient.xAstroBoy;
    using ClientActions;
    using Il2CppSystem.Collections;
    using QM.Buttons;
    using QM.Carousel.Items;
    using QM.Wings;
    using UnityEngine;
    using IEnumerator = System.Collections.IEnumerator;

    internal class ApiChecker : AstroEvents
    {
        internal override void ExecutePriorityPatches()
        {
            Log.Debug("Starting ApiBase Check!");
            APIBase.ApiCheck((() => { ClientEventActions.OnMenuApiReady.SafetyRaise(); }));
        }
    }

    public static class APIBase
    {


        public class Events
        {
            public static Action<VRCToggle, bool> onVRCToggleValChange = new Action<VRCToggle, bool>((er, str) => { });
            public static Action<QMCToggle, bool> onQMCToggleValChange = new Action<QMCToggle, bool>((er, str) => { });
            public static Action<QMCSlider, bool> onQMCSliderToggleValChange = new Action<QMCSlider, bool>((er, str) => { });
        } 
        /// <summary>
        ///  Set this if u want to override what happens when a button/ tgl throws an error
        /// </summary>
        public static Action<Exception, string> ErrorCallBack { get; set; } = new Action<Exception, string>((er, str) => {
            Log.Error($"The ButtonAPI had an Error At {str}, {er}");
        });

        private static bool HasChecked;

        public static bool IsReady()
        {
            var type = typeof(ApiPaths);
            var props = type.GetProperties(BindingFlags.Public | BindingFlags.Static);
            var missing = new List<string>();

            foreach (var prop in props)
            {
                object val;
                try
                {
                    val = prop.GetValue(null);
                }
                catch
                {
                    missing.Add($"{prop.Name} is Null");
                    continue;
                }

                if (val == null)
                    missing.Add(prop.Name);
                //else
                    //Log.Debug($"ApiPaths: {prop.Name} Found");
            }

            if (missing.Count > 0)
            {
                //foreach (var name in missing)
                //    Log.Error($"The APIBase is not ready, {name}");
                return false;
            }

            HasChecked = true;
            return true;
        }


        internal static IEnumerator OnApiReady(Action action)
        {
            while (!IsReady())
            {
                yield return new WaitForSeconds(0.1f);
            }
            action.Invoke();
        }


        internal static void ApiCheck(Action action)
        {
            MelonLoader.MelonCoroutines.Start(OnApiReady(action));
        }

        internal static void SafelyInvoke(Action action, string name)
        {
            try
            {
                action.SafetyRaise();
            }
            catch (System.Exception e)
            {
                ErrorCallBack.Invoke(e, name);
            }
        }

        internal static void SafelyInvoke(bool state, System.Action<bool> action, string name)
        {
            try
            {
                action.SafetyRaise(state);
            }
            catch (System.Exception e)
            {
                ErrorCallBack.Invoke(e, name);
            }
        }
    }

}


