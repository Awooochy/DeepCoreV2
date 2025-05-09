namespace AstroClient.MenuApi.ActionMenuAPI.AMXref
{
    using System.Reflection;
    using UnityEngine;

    internal static class MenuIconUtils
    {
        private const BindingFlags InstanceFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        internal static FieldInfo GetFieldInfo(MenuIconId id)
        {
            return typeof(ActionMenuController.MenuIcons).GetField(id.ToString(), InstanceFlags);
        }

        internal static Texture2D GetIcon(ActionMenuController.MenuIcons icons, MenuIconId id)
        {
            if (icons == null) return null;
            FieldInfo fi = GetFieldInfo(id);
            return fi == null ? null : (Texture2D)fi.GetValue(icons);
        }
    }
}