using System.Collections;
using DeepCore.Client.Patching;
using UnityEngine;

namespace DeepCore.Client.UI
{
    internal class UIController
    {
        public static IEnumerator WaitForQM()
        {
            while (GameObject.Find("Canvas_QuickMenu(Clone)") == null)
            {
                yield return null;
            }
            NetworkManagerPatch.Patch();
            QM.QMUI.InitQM();
            while (GameObject.Find("Canvas_MainMenu(Clone)") == null)
            {
                yield return null;
            }
            MM.MMUI.WaitForMM();
        }
    }
}
