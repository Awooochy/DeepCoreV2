// ────────────────────────────────────────────────────────────────────────────────
//  1)  Strongly‑typed identifier for every field in ActionMenuController.MenuIcons
// ────────────────────────────────────────────────────────────────────────────────

namespace AstroClient.MenuApi.ActionMenuAPI.AMXref
{
    using System;
    using System.Linq;
    using System.Reflection;
    using UnhollowerRuntimeLib.XrefScans;

    // ────────────────────────────────────────────────────────────────────────────────
//  2)  Utilities: enum → FieldInfo, enum → Texture2D, etc.
// ────────────────────────────────────────────────────────────────────────────────

internal static class AMMenuXrefs
    {
        internal static bool Uses(this MethodBase method, FieldInfo field)
        {
            if (method == null || field == null) return false;
            try { return XrefScanner.XrefScan(method).Any(x => ReferenceEquals(x.TryResolve(), field)); }
            catch { return false; }
        }

        internal static MethodInfo FindAMMethod(this Type actionMenuType, FieldInfo targetField)
        {
            return actionMenuType.GetMethods(BindingFlags.Instance | BindingFlags.Static |
                                             BindingFlags.Public | BindingFlags.NonPublic)
                .First(m => m.Name.StartsWith("Method", StringComparison.Ordinal) &&
                            m.Uses(targetField));
        }

        // enum overload
        internal static MethodInfo FindAMMethod(this Type actionMenuType, MenuIconId id)
        {
            FieldInfo fi = MenuIconUtils.GetFieldInfo(id);
            if (fi == null) throw new ArgumentException($"No field for {id}");
            return actionMenuType.FindAMMethod(fi);
        }
    }
}