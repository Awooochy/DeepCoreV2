namespace AstroClient.MenuApi
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;
    using MelonLoader;
    using UnhollowerBaseLib;
    using UnhollowerRuntimeLib.XrefScans;

    internal static class XRefExts
    {
        // A helper function to safely resolve an object from an xref  
        private static Il2CppSystem.Object ResolveXrefToObject(XrefInstance xref)
        {
            try
            {
                if (xref.Type == XrefType.Global)
                {
                    var ptr = Marshal.ReadIntPtr(xref.Pointer);
                    if (ptr == IntPtr.Zero)
                    {
                        // If the pointer is null, we try to spawn a new instance of the class  
                        var newObj = SpawnNewClassInstance(xref);
                        return newObj ?? null;
                    }

                    return new Il2CppSystem.Object(ptr); // Return resolved object if pointer is valid  
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Error resolving xref to object: {ex.Message}");
            }

            return null;
        }

        // A helper function to dynamically spawn a new instance of a class  
        private static Il2CppSystem.Object SpawnNewClassInstance(XrefInstance xref)
        {
            try
            {
                // Here we assume that the Xref instance type is valid for spawning an instance  
                // This assumes we can resolve the type of the xref instance (you might need  
                // to adjust this logic based on your actual object model)  
                var type = xref.TryResolve()?.ReflectedType;
                if (type != null)
                {
                    // Dynamically instantiate the type (assuming it has a parameterless constructor)  
                    var newInstance = Activator.CreateInstance(type);
                    if (newInstance != null)
                    {
                        return new Il2CppSystem.Object((IntPtr)newInstance);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error($"Failed to spawn new class instance: {ex.Message}");
            }

            return null; // If spawning failed, return null  
        }

        // Checks if any global references in the method match the search term  
        internal static bool XRefScanForGlobal(this MethodBase methodBase, string searchTerm, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                Log.Error($"XRefScanForGlobal \"{methodBase}\" has an empty searchTerm. Returning false");
                return false;
            }

            return XrefScanner.XrefScan(methodBase).Any(xref =>
            {
                var obj = ResolveXrefToObject(xref);
                return obj?.ToString().IndexOf(searchTerm,
                    ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0;
            });
        }

        // Checks if method name or parent type matches any reference in the xref scan  
        internal static bool XRefScanForMethod(this MethodBase methodBase, string methodName = null,
            string parentType = null, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(methodName) && string.IsNullOrEmpty(parentType))
            {
                Log.Error($"XRefScanForMethod \"{methodBase}\" has all null/empty parameters. Returning false");
                return false;
            }

            return XrefScanner.XrefScan(methodBase).Any(xref =>
            {
                if (xref.Type != XrefType.Method) return false;

                var resolved = xref.TryResolve();
                if (resolved == null) return false;

                var matchesMethod = !string.IsNullOrEmpty(methodName) && resolved.Name.IndexOf(methodName,
                    ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0;
                var matchesType = !string.IsNullOrEmpty(parentType) && resolved.ReflectedType?.Name.IndexOf(parentType,
                    ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0;

                return matchesMethod || matchesType;
            });
        }

        // Returns the count of method references matching the given method name or parent type  
        internal static int XRefScanMethodCount(this MethodBase methodBase, string methodName = null,
            string parentType = null, bool ignoreCase = true)
        {
            if (string.IsNullOrEmpty(methodName) && string.IsNullOrEmpty(parentType))
            {
                Log.Error($"XRefScanMethodCount \"{methodBase}\" has all null/empty parameters. Returning -1");
                return -1;
            }

            return XrefScanner.XrefScan(methodBase).Count(xref =>
            {
                if (xref.Type != XrefType.Method) return false;

                var resolved = xref.TryResolve();
                if (resolved == null) return false;

                var matchesMethod = !string.IsNullOrEmpty(methodName) && resolved.Name.IndexOf(methodName,
                    ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0;
                var matchesType = !string.IsNullOrEmpty(parentType) && resolved.ReflectedType?.Name.IndexOf(parentType,
                    ignoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal) >= 0;

                return matchesMethod || matchesType;
            });
        }


        // Checks if any of the provided keywords match global references in the method  
        public static bool CheckXref(MethodInfo m, string[] keywords)
        {
            try
            {
                foreach (var keyword in keywords)
                    if (!XrefScanner.XrefScan(m).Any(
                            instance =>
                            {
                                if (instance.Type == XrefType.Global)
                                {
                                    var obj = instance.ReadAsObject();
                                    if (obj != null)
                                    {
                                        var targetClass =
                                            (IntPtr)System.Runtime.InteropServices.Marshal.ReadInt64(obj.Pointer);
                                        if (targetClass == Il2CppClassPointerStore<string>
                                                .NativeClassPtr)
                                            return obj.ToString().Equals(keyword, StringComparison.OrdinalIgnoreCase);
                                    }
                                }

                                return false;
                            }))
                        return false;

                return true;
            }
            catch
            {
            }

            return false;
        }

        internal static bool CheckXref(MethodInfo m, List<string> keywords)
        {
            return CheckXref(m, keywords.ToArray());
        }
    }
}
