using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeepCore.Client.Coroutine
{
    internal class CustomStandardPopup
    {
        public static IEnumerator Init()
        {
            while (GameObject.Find("MenuContent/Popups/StandardPopup") == null)
            {
                yield return null;
            }
            GameObject.Find("MenuContent/Popups/StandardPopup/TitleText").GetComponent<TextMeshProUGUI>().color = Color.red;
            GameObject.Find("MenuContent/Popups/StandardPopup/BodyText").GetComponent<TextMeshProUGUI>().color = Color.red;
            GameObject.Find("MenuContent/Popups/StandardPopup/RingGlow").GetComponent<Image>().color = Color.red;
            GameObject.Find("MenuContent/Popups/StandardPopup/InnerDashRing").GetComponent<Image>().color = Color.red;
            GameObject.Find("MenuContent/Popups/StandardPopup/LowPercent").SetActive(false);
            GameObject.Find("MenuContent/Popups/StandardPopup/HighPercent").SetActive(false);
            GameObject.Find("MenuContent/Popups/StandardPopup/ProgressLine").SetActive(false);
            GameObject.Find("MenuContent/Popups/StandardPopup/ArrowLeft").SetActive(false);
            GameObject.Find("MenuContent/Popups/StandardPopup/Rectangle").SetActive(false);
            yield break;
        }
    }
}
