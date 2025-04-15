using System.Collections;
using DeepCore.Client.Misc;
using MelonLoader;
using UnityEngine;

namespace DeepCore.Client.Module.Funnies
{
    internal class RussianRoulette
    {
        public static void RouletteStart()
        {
            int[] array = new int[6];
            int[] array2 = new int[6];
            System.Random random = new System.Random();
            int num = random.Next(0, 6);
            int num2 = random.Next(0, 6);
            array[num] = 1;
            array2[num2] = 1;
            VrcExtensions.HudNotif("[Russian Roulette]: Good luck!");
            VrcExtensions.HudNotif("[Russian Roulette]: Ready?");
            bool flag = false;
            int num3 = 0;
            int num4 = 1;
            do
            {
                bool flag2 = array2[num3] == 1;
                if (flag2)
                {
                    VrcExtensions.HudNotif(string.Format("[Russian Roulette] *Bang* - Computer has lost, You win on round {0}", num4));
                    flag = true;
                }
                else
                {
                    bool flag3 = array[num3] == 1;
                    if (flag3)
                    {
                        VrcExtensions.HudNotif(string.Format("[Russian Roulette] *Bang* - Computer wins on round {0}, you lost!", num4));
                        flag = true;
                        MelonCoroutines.Start(RouletteLost());
                    }
                    else
                    {
                        VrcExtensions.HudNotif($"[Russian Roulette] *Click* - You both survived round {num4}");
                        num3++;
                        num4++;
                    }
                }
            }
            while (!flag);
        }
        private static IEnumerator RouletteLost()
        {
            yield return new WaitForSeconds(5f);
            //ApplicationUtils.OpenLinkInBrowser("https://www.youtube.com/watch?v=Xzx3-hKl__8");
            yield break;
        }
    }
}
