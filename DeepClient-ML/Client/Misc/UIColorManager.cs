using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.SDKBase;

namespace DeepCore.Client.Misc
{
    internal class UIColorManager
    {
        public static float HRed = 1f;
        public static float HGreen = 0f;
        public static float HBlue = 0.592156863f;
        public static void SetRed()
        {
            PopupHelper.PopupCall("UI Color", "Enter color values with commas (e.g., 0.10, 0.25, 0.75)...", "Set", false, userInput =>
            {
                var colorValues = userInput.Split(',');
                if (colorValues.Length == 3)
                {
                    if (float.TryParse(colorValues[0], out float red) &&
                        float.TryParse(colorValues[1], out float green) &&
                        float.TryParse(colorValues[2], out float blue))
                    {
                        HRed = red;
                        HGreen = green;
                        HBlue = blue;
                        Recolorthem();
                    }
                    else
                    {
                        DeepConsole.Log("UIColor", "Invalid color values entered. Please enter decimal numbers.");
                    }
                }
                else
                {
                    DeepConsole.Log("UIColor", "Please enter exactly three values.");
                }
            });
        }
        public static void Recolorthem()
        {
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Panel_Backdrop").GetComponent<Image>().color = new Color(HRed,HGreen, HBlue);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left").GetComponent<Image>().color = new Color(HRed, HGreen, HBlue);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Right").GetComponent<Image>().color = new Color(HRed, HGreen, HBlue);
            GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_Lighting (1)/Point light").GetComponent<Light>().color = new Color(HRed, HGreen, HBlue);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/GoButton/Text").GetComponent<TextMeshProUGUI>().color = new Color(HRed, HGreen, HBlue);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/txt_Percent").GetComponent<TextMeshProUGUI>().color = new Color(HRed, HGreen, HBlue);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/txt_LOADING_Size").GetComponent<TextMeshProUGUI>().color = new Color(HRed, HGreen, HBlue);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR_BG").GetComponent<Image>().color = new Color(0f, 0f, 0f, 0f);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR").GetComponent<Image>().color = new Color(HRed, HGreen, HBlue);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Loading Elements/LOADING_BAR").GetComponent<Image>().color = new Color(HRed, HGreen, HBlue);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ButtonMiddle/Text").GetComponent<TextMeshProUGUI>().color = new Color(0.5389f, 0f, 0f, 1f);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Panel_Backdrop").GetComponent<Image>().color = new Color(HRed, HGreen, HBlue);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Left").GetComponent<Image>().color = new Color(HRed, HGreen, HBlue);
            GameObject.Find("MenuContent/Popups/LoadingPopup/ProgressPanel/Parent_Loading_Progress/Decoration_Right").GetComponent<Image>().color = new Color(HRed, HGreen, HBlue);
            GameObject.Find("MenuContent/Popups/LoadingPopup/3DElements/LoadingBackground_TealGradient/_Lighting (1)/Point light").GetComponent<Light>().color = new Color(HRed, HGreen, HBlue);
        }
    }
}
