namespace AstroClient.MenuApi.Tools
{
    using System;
    using System.Linq;
    using System.Reflection;
    using AstroClient.xAstroBoy.Utility;
    using Il2CppSystem.Collections.Generic;
    using UnityEngine;
    using VRC.UI.Elements;
    using VRC.UI.Elements.Controls;
    using Object = UnityEngine.Object;

    internal static class PageGen
    {

        // Lazily initialize the constructor info, with a fallback scan if the direct lookup returns null
        static readonly ConstructorInfo InterfaceCtor = InitCtor();

        private static ConstructorInfo InitCtor()
        {
            var ifaceType = typeof(InterfacePublicAbstractObBoObVoStObInVoStBoUnique);
            // Try the simple lookup first
            var ctor = ifaceType.GetConstructor(
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic,
                binder: null,
                types: new[] { typeof(IntPtr) },
                modifiers: null);

            if (ctor != null)
                return ctor;

            // Fallback: scan all ctors for one taking exactly one IntPtr
            ctor = ifaceType
                .GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .FirstOrDefault(c =>
                {
                    var ps = c.GetParameters();
                    return ps.Length == 1 && ps[0].ParameterType == typeof(IntPtr);
                });

            if (ctor == null)
                Log.Debug($"ToInterfaceInstance: no ctor(IntPtr) found on {ifaceType.Name}");
            return ctor;
        }

        internal static InterfacePublicAbstractObBoObVoStObInVoStBoUnique ToInterfaceInstance(
            this MenuStateController controller)
        {
            if (controller == null)
            {
                Log.Debug("ToInterfaceInstance: controller is null");
                return null;
            }

            if (InterfaceCtor == null)
            {
                // we already logged in InitCtor
                return null;
            }

            try
            {
                // invoke the ctor(IntPtr) to wrap the Il2CppObject
                return (InterfacePublicAbstractObBoObVoStObInVoStBoUnique)InterfaceCtor.Invoke(new object[]
                    { controller.Pointer });
            }
            catch (Exception e)
            {
                Log.Debug($"ToInterfaceInstance: ctor.Invoke failed: {e}");
                return null;
            }
        }



        internal static UIPage LinkPage(this UIPage page, MenuStateController controller)
        {
            if (page == null)
                return null;

            page.field_Protected_InterfacePublicAbstractObBoObVoStObInVoStBoUnique_0 =
                controller.ToInterfaceInstance();
            return page;
        }

        internal static UIPage GenerateQuickMenuPage(this GameObject nested, MenuStateController controller, string menuName)
        {

            UIPage result = null;
            if (nested != null)
            {
                result = nested.GetOrAddComponent<UIPage>();
                if (result != null)
                {
                    result.name = menuName;
                    result.LinkPage(controller);
                    result.SetName(menuName);
                    result.field_Private_Boolean_1 = true;
                    result.field_Private_List_1_UIPage_0 = new List<UIPage>();
                    result.field_Private_List_1_UIPage_0.Add(result);
                    controller.AddPage(result);
                }
            }
            result.enabled = true;
            return result;
        }

        internal static UIPage GeneratePage(this UIPage template, MenuStateController controller, string menuName)
        {

            UIPage result = null;
            if (template != null)
            {
                result = Object.Instantiate(template, template.transform.parent, true);
                result.gameObject.EnableUIComponents();
                if (result != null)
                {
                    result.name = menuName;
                    result.LinkPage(controller);
                    result.SetName(menuName);
                    result.field_Private_Boolean_1 = true;
                    result.field_Private_List_1_UIPage_0 = new List<UIPage>();
                    result.field_Private_List_1_UIPage_0.Add(result);
                    controller.AddPage(result);
                }
            }
            result.enabled = true;
            return result;
        }


        internal static void SetupPage(this UIPage page, MenuStateController controller, string menuName)
        {
            if (page != null)
            {
                page.name = menuName;
                page.LinkPage(controller);
                page.SetName(menuName);
                page.field_Private_Boolean_1 = true;
                page.field_Private_List_1_UIPage_0 = new List<UIPage>();
                page.field_Private_List_1_UIPage_0.Add(page);
                controller.AddPage(page);
            }
        }

        internal static void AddPage(this MenuStateController controller, UIPage page)
        {
            if (page != null)
            {
                var list = controller.field_Public_ArrayOf_UIPage_0.ToList();
                if (!list.Contains(page))
                {
                    list.Add(page);
                    controller.field_Public_ArrayOf_UIPage_0 = list.ToArray();
                }
                var dict = controller.field_Private_Dictionary_2_String_UIPage_0;
                if (!dict.ContainsKey(page.name))
                {
                    dict.Add(page.name, page);
                    controller.field_Private_Dictionary_2_String_UIPage_0 = dict;
                }
            }
        }

        internal static void RemovePage(this MenuStateController controller, UIPage page, bool DestroyPage = true)
        {
            if (page != null)
            {
                var list = controller.field_Public_ArrayOf_UIPage_0.ToList();
                if (list.Contains(page))
                {
                    list.Remove(page);
                    if (DestroyPage)
                    {
                        UnityEngine.Object.Destroy(page);
                    }
                    controller.field_Public_ArrayOf_UIPage_0 = list.ToArray();
                }
            }
        }




    }
}