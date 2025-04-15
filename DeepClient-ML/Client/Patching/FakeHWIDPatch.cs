using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace DeepCore.Client.Patching
{
    internal class FakeHWIDPatch
    {
        public static string newHWID = "";
        public static void Patch()
        {
            EasyPatching.EasyPatchMethodPost(typeof(SystemInfo), "deviceUniqueIdentifier", typeof(FakeHWIDPatch), "FakeHWID");
        }
        public static bool FakeHWID(ref string __result)
        {
            if (newHWID == string.Empty)
            {
                newHWID = KeyedHashAlgorithm.Create().ComputeHash(Encoding.UTF8.GetBytes(string.Format("{0}A-{1}{2}-{3}{4}-{5}{6}-3C-1F", new object[]
                {
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9),
                    new System.Random().Next(0, 9) }))).Select(delegate (byte x)
                    {
                        byte b = x;
                        return b.ToString("x2");
                    }).Aggregate((string x, string y) => x + y);

                DeepConsole.Log($"Patch","[Spoofer]: Success Patched HWID {newHWID}");
            }
            __result = newHWID;
            return false;
        }
    }
}
