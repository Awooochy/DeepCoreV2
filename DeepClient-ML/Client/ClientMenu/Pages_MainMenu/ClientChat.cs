using System.Collections.Generic;
using System.Media;
using DeepCore.Client.Misc;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
    internal class ClientChat
    {
        internal static bool isReady = false;
        internal static bool isNotif = false;
        internal static TextMeshProUGUI logText;
        internal static Queue<string> messages = new Queue<string>();
        internal static GameObject logObject;
        internal static Vector2 consoleSize = new Vector2(650f, 800f);
        public static void ClientChatMenu(UiManager UIManager)
        {
            ReMenuPage reCategoryPage = UIManager.QMMenu.AddMenuPage("Client Chat", null);
            reCategoryPage.AddButton("Send a message", "it send a message wow.", delegate
            {
                PopupHelper.PopupCall("Chat Message", "Enter an text", "Send", false, userInput =>
                {
                    Module.RPC.RPCManager.SendRPC($"DCChat:{userInput}");
                });
            });
            reCategoryPage.AddSpacer();reCategoryPage.AddSpacer(); FormChatLog();
            reCategoryPage.AddButton("Clear", "Delete all message (Local).", delegate
            {
                logText.text = "";
            });
            reCategoryPage.OnOpen += PlaceConsole;
        }
        public static void FormChatLog()
        {
            bool flag = isReady;
            if (!flag)
            {
                GameObject parentObject = GameObject.Find("Canvas_QuickMenu(Clone)/CanvasGroup/Container/Window/QMParent/Menu_ClientChat/Scrollrect/Viewport/VerticalLayoutGroup");
                logObject = new GameObject("BestChat");
                logObject.transform.SetParent(parentObject.transform, false);
                GameObject box = new GameObject("Box");
                box.transform.SetParent(logObject.transform, false);
                Image boxImage = box.AddComponent<Image>();
                boxImage.color = new Color(0f, 0f, 0f, 0.5f);
                RectMask2D mask = box.AddComponent<RectMask2D>();
                RectTransform boxRect = box.GetComponent<RectTransform>();
                boxRect.sizeDelta = consoleSize;
                boxRect.anchorMin = new Vector2(0f, 1f);
                boxRect.anchorMax = new Vector2(0f, 1f);
                boxRect.pivot = new Vector2(0f, 1f);
                boxRect.anchoredPosition = Vector2.zero;
                box.transform.localPosition = Vector2.zero;
                ScrollRect scrollRect = logObject.AddComponent<ScrollRect>();
                scrollRect.horizontal = false;
                scrollRect.vertical = true;
                GameObject viewport = new GameObject("Viewport");
                viewport.transform.SetParent(logObject.transform, false);
                RectTransform viewportRect = viewport.AddComponent<RectTransform>();
                viewportRect.sizeDelta = consoleSize;
                viewportRect.anchorMin = new Vector2(0f, 1f);
                viewportRect.anchorMax = new Vector2(0f, 1f);
                viewportRect.pivot = new Vector2(0f, 1f);
                viewportRect.anchoredPosition = Vector2.zero;
                viewport.AddComponent<CanvasRenderer>();
                viewport.AddComponent<Image>();
                viewport.AddComponent<Mask>().showMaskGraphic = false;
                scrollRect.viewport = viewportRect;
                viewport.transform.localPosition = Vector2.zero;
                GameObject content = new GameObject("Content");
                content.transform.SetParent(viewport.transform, false);
                RectTransform contentRect = content.AddComponent<RectTransform>();
                contentRect.sizeDelta = consoleSize;
                logText = content.AddComponent<TextMeshProUGUI>();
                logText.text = "";
                logText.fontSize = 23f;
                logText.alignment = TextAlignmentOptions.BottomLeft;
                logText.color = Color.white;
                logText.enableWordWrapping = true;
                logText.rectTransform.sizeDelta = consoleSize;
                logText.rectTransform.anchorMin = new Vector2(0f, 1f);
                logText.rectTransform.anchorMax = new Vector2(0f, 1f);
                logText.rectTransform.pivot = new Vector2(0f, 1f);
                logText.rectTransform.anchoredPosition = Vector2.zero;
                logText.transform.localPosition = Vector2.zero;
                scrollRect.content = contentRect;
                box.transform.localPosition = Vector2.zero;
                viewport.transform.localPosition = Vector2.zero;
                content.transform.localPosition = Vector2.zero;
                logText.transform.localPosition = Vector2.zero;
                logObject.transform.position = new Vector3(2.8513f, 1.151f, 5.9919f);
                logObject.transform.localPosition = new Vector3(-154.1549f, -95.8792f, -0.3713f);
                isReady = true;
            }
        }
        public static void PlaceConsole()
        {
            logObject.transform.position = new Vector3(2.8513f, 1.151f, 5.9919f);
            logObject.transform.localPosition = new Vector3(-154.1549f, -95.8792f, -0.3713f);
        }
        internal static void ChatMSG(string username, string message)
        {
            VrcExtensions.Toast($"{username}", message);
            SystemSounds.Asterisk.Play();
            bool boldQMConsole = false;
            if (boldQMConsole)
            {
                messages.Enqueue("<b>" + message + "<b>\n");
            }
            else
            {
                messages.Enqueue($"[{username}]|"+message+"\n");
            }
            string allMessages = string.Concat(messages);
            bool flag2 = allMessages.Length > 10000;
            if (flag2)
            {
                string text = allMessages;
                int length = text.Length;
                int num = length - 10000;
                allMessages = text.Substring(num, length - num);
            }
            bool boldQMConsole2 = false;
            if (boldQMConsole2)
            {
                logText.text = "<b>"+allMessages+"<b>\n";
            }
            else
            {
                logText.text = allMessages ?? "";
            }
        }
    }
}
