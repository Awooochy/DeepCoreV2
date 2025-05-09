//───────────────────────────────────────────────────────────────────────────────
//  Action‑Menu Blind Hook (final)
//
//  • finds every ActionMenu.Method* that is **public|non‑public void ()**
//    and whose name does NOT contain “_Private_”.
//  • at run‑time it *emits* one static  void  prefix per target via
//    Reflection‑Emit.  Each prefix calls Log.Debug("… <target> …").
//
//  This satisfies all constraints:
//
//      1. prefix signature is `void` with **no parameters**
//      2. prefix is truly **static** (Harmony requirement)
//      3. no stack‑trace is used
//───────────────────────────────────────────────────────────────────────────────
using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using AstroClient;
using AstroClient.xAstroBoy.Patching;
using HarmonyLib;

internal sealed class ActionMenuBlindHook : AstroEvents
{
    internal override void ExecutePriorityPatches()
    {
        try
        {
            //PatchAll();
        }
        catch (Exception e) { Log.Error($"ActionMenuBlindHook failed: {e}"); }
    }

    private static void PatchAll()
    {
        // resolve Log.Debug(string) with its optional parameters so we can emit a direct call
        MethodInfo logDebug = typeof(Log).GetMethod(
                                  nameof(Log.Debug),
                                  BindingFlags.Public | BindingFlags.Static,
                                  binder: null,
                                  types: new[] { typeof(string), typeof(string), typeof(int) },
                                  modifiers: null);

        if (logDebug == null)
        {
            Log.Error("Log.Debug(string,string,int) not found – cannot blind‑hook ActionMenu");
            return;
        }

        // dynamic assembly / module to host our generated prefixes
        var asmName = new AssemblyName("AM_BH_Generated");
        var asmBuilder = AssemblyBuilder.DefineDynamicAssembly(asmName, AssemblyBuilderAccess.Run);
        var modBuilder = asmBuilder.DefineDynamicModule("AM_BH_Generated_Module");

        var targets = typeof(ActionMenu)
            .GetMethods(BindingFlags.Instance | BindingFlags.Static |
                        BindingFlags.Public | BindingFlags.NonPublic)
            .Where(m => m.Name.StartsWith("Method") &&
                        !m.Name.Contains("_Private_") &&
                        m.ReturnType == typeof(void) &&
                        m.GetParameters().Length == 0)
            .ToArray();

        foreach (MethodInfo target in targets)
        {
            // define a holder type with a single static prefix
            TypeBuilder tb = modBuilder.DefineType($"PFX_{target.Name}",
                                   TypeAttributes.Public | TypeAttributes.Class | TypeAttributes.Sealed | TypeAttributes.Abstract);

            MethodBuilder mb = tb.DefineMethod($"Prefix_{target.Name}",
                                   MethodAttributes.Public | MethodAttributes.Static,
                                   typeof(void),
                                   Type.EmptyTypes);

            ILGenerator il = mb.GetILGenerator();
            il.Emit(OpCodes.Ldstr, $"[ActionMenu] ⇒ {target.Name}"); // msg
            il.Emit(OpCodes.Ldstr, "");                              // callerName  (empty)
            il.Emit(OpCodes.Ldc_I4_0);                               // callerLine  (0)
            il.Emit(OpCodes.Call, logDebug);                         // Log.Debug(...)
            il.Emit(OpCodes.Ret);

            // create the type & fetch the MethodInfo
            MethodInfo prefix = tb.CreateType().GetMethod(mb.Name);

            // patch
            new AstroPatch(target, new HarmonyMethod(prefix), null);
        }

        Log.Write($"[ActionMenuBlindHook] patched {targets.Length} ActionMenu Methods");
    }
}
