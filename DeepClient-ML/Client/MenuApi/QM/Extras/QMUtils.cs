namespace AstroClient.MenuApi.QM.Extras
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography.Xml;
    using UnityEngine;
    using VRC.UI.Elements;
    using VRC.UI.Elements.Controls;
    using Object = UnityEngine.Object;
    using Transform = UnityEngine.Transform;

    public static class QMUtils
    {





        public static void DestroyChildren(this Transform transform, Func<Transform, bool> exclude = null)
        {
            for (var childcount = transform.childCount - 1; childcount >= 0; childcount--)
                if (exclude == null || exclude(transform.GetChild(childcount)))
                    Object.DestroyImmediate(transform.GetChild(childcount).gameObject);
        }

        public static void DestroyChildren(this GameObject gameObj, Func<Transform, bool> exclude = null) =>
            gameObj.transform.DestroyChildren(exclude);

        public static List<GameObject> GetChildren(this Transform transform)
        {
            List<GameObject> list = new List<GameObject>();
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject gameObject = transform.GetChild(i).gameObject;
                list.Add(gameObject);
            }
            return list;
        }

        internal static bool IsObfuscated(this string str)
        {
            foreach (var it in str)
                if (!char.IsDigit(it) && !((it >= 'a' && it <= 'z') || (it >= 'A' && it <= 'Z')) && it != '_' &&
                    it != '`' && it != '.' && it != '<' && it != '>')
                    return true;

            return false;
        }

        public static void ResetTransform(Transform transform)
        {
            transform.localRotation = Quaternion.Euler(0, 0, 0);
            transform.localScale = new Vector3(1f, 1f, 1f);
            transform.localPosition = Vector3.zero;
        }

        public static void RemoveUnknownComps(GameObject gameObject, Action<string> callBackOnDestroy = null)
        {
            Component[] components = gameObject.GetComponents<Component>();
            for (int D = 0; D < components.Length; D++)
            {
                var name = components[D].GetIl2CppType().Name;

                if (name.IsObfuscated() && components[D].GetIl2CppType().BaseType.Name != nameof(TMPro.TextMeshProUGUI))
                {
                    Object.Destroy(components[D]);
                    callBackOnDestroy.Invoke(name);
                }
            }
        }

        public static void OpenPage(this MenuStateController control, string page = "QuickMenuDashboard", UIPage.TransitionType transition = UIPage.TransitionType.Right)
        {
            if (control == null) return;
            if (page == null) return;
            control.Method_Public_Void_String_UIContext_Boolean_TransitionType_0(page, null, false, transition);
        }

        public static void ClosePage(this UIPage page)
        {
            if (page == null) return;
            page.Method_Protected_Virtual_New_Void_0();
        }


        public static GameObject AppendChild(this GameObject parent, string name)
        {
            return parent.transform.AppendChild(name).gameObject;
        }
        public static Transform AppendChild(this Transform parent, string name)
        {
            var transf = new GameObject(name).transform;
            transf.SetParent(parent);
            transf.localPosition = Vector3.zero;
            transf.localScale = Vector3.one;
            transf.localRotation = Quaternion.identity;
            return transf;
        }



    }
}

