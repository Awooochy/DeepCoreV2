using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace DeepClient.Client.ClientMenu
{
    internal class QMConsole
    {
        public static TextMeshProUGUI logText;
        public static int maxLines = 28;
        public static string logContent = "";
        public static int currentLines = 0;
        public static bool isloaded = false;
        public static IEnumerator StartConsole()
        {
            while (GameObject.Find("Canvas_QuickMenu(Clone)") == null)
            {
                yield return null;
            }
            new GameObject("DConsole").transform.parent = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon").transform;
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").transform.position = new Vector3(-1.2201f, 0.7617f, 252.4173f);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").transform.localPosition = new Vector3(615.0677f, -185.995f, -353.3717f);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").transform.localScale = new Vector3(7f, 7f, 7f);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").AddComponent<TextMeshProUGUI>();
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").GetComponent<TextMeshProUGUI>().fontSize = 3f;
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").GetComponent<TextMeshProUGUI>().text = "If you see this it mean the console is set :>, But No Log :/";
            /*
            new GameObject("Backgrund").transform.parent = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/RConsole").transform;
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/RConsole/Backgrund").transform.position = new Vector3(-1.1295f, 0.6706f, 252.2481f);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/RConsole/Backgrund").transform.localPosition = new Vector3(-59.0782f, -32.5392f, -0.0143f);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/RConsole/Backgrund").transform.localRotation = Quaternion.Euler(0f, 0f, 0f);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/RConsole/Backgrund").transform.localScale = new Vector3(0.8255f, 1.1619f, 0f);
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/RConsole/Backgrund").AddComponent<Image>();
            */
            isloaded = true;
            yield break;
        }
        public static void ConsoleVisuabillity(bool s)
        {
            if (isloaded)
            {
                GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").SetActive(s);
            }
        }
        public static void ClearLog()
        {
            logContent = "";
            currentLines = 0;
            if (logText != null)
            {
                logText.text = "";
            }
            else
            { }
            GameObject logObject = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole");
            if (logObject != null)
            {
                TextMeshProUGUI logComponent = logObject.GetComponent<TextMeshProUGUI>();
                if (logComponent != null)
                {
                    logComponent.text = "";
                }
                else
                { }
            }
            else
            { }
        }
        public static void PrintLog(string Name, string Content)
        {
            if (isloaded)
            {
                LogMessage(Name, Content);
            }
        }
        public static void LogMessage(string yes, string message)
        {
            DateTime now = DateTime.Now;
            if (currentLines >= maxLines)
            {
                ClearLog();
            }
            logContent += $"<color=white>[</color><color=yellow>{now:HH:mm}</color><color=white>]</color> <color=white>[</color><color=green>{yes}</color><color=white>]</color> {message}\n";
            GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/Wing_Right/Button/Icon/DConsole").GetComponent<TextMeshProUGUI>().text = logContent;
            currentLines++;
        }
    }
}
