using System;
using System.IO;
using System.Linq;
using UnityEngine;
using VRC.Core;
using VRC.SDKBase;
using VRC.Udon;

namespace DeepClient.Client
{
    public static class ClientUtils
    {
        public static int udoncounts = 0;
        internal static GameObject FindUIObject(this GameObject parent, string name)
        {
            if (parent == null) return null;
            Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
            foreach (var t in trs)
                if (t.name == name)
                    return t.gameObject;
            return null;
        }
        public static void LogUdon()
        {
            string text = null;
            foreach (UdonBehaviour udonBehaviour in UnityEngine.Object.FindObjectsOfType<UdonBehaviour>())
            {
                foreach (Il2CppSystem.Collections.Generic.KeyValuePair<string, Il2CppSystem.Collections.Generic.List<uint>> keyValuePair in udonBehaviour._eventTable)
                {
                    text = string.Concat(new string[]
                    {
                        text,
                        Environment.NewLine,
                        "Name:'",
                        udonBehaviour.gameObject.name,
                        "' Key: '",
                        keyValuePair.key,
                        "'"
                    });
                    udoncounts++;
                }
            }
            File.WriteAllText("DeepClient/UdonLog.txt", text);
            DeepConsole.Log("UdonLogger", $"Logged {udoncounts} UdonKey.");
            udoncounts = 0;
        }
        public static void LogItems()
        {
            string text = null;
            int itemCount = 0;

            foreach (VRC_Pickup gameObject in Resources.FindObjectsOfTypeAll<VRC_Pickup>())
            {
                text = string.Concat(new string[]
                {
            text,
            Environment.NewLine,
            "Name: '", gameObject.name, "' "
                });
                itemCount++;
            }
            File.WriteAllText("DeepClient/Pickups.txt", text);
            DeepConsole.Log("ItemLogger", $"Logged {itemCount} Items.");
        }
        public static void SaveFrinds()
        {
            string filePath = "DeepClient\\FriendsIDBackups.txt";
            System.Collections.Generic.List<string> currentFriendIDs = APIUser.CurrentUser.friendIDs.ToArray().ToList();
            int currentFriendCount = currentFriendIDs.Count;
            if (File.Exists(filePath))
            {
                string[] existingLines = File.ReadAllLines(filePath);
                if (currentFriendCount > existingLines.Length)
                {
                    File.WriteAllLines(filePath, currentFriendIDs);
                    DeepConsole.Log("FriendSaver", $"{currentFriendCount} friendIDs saved.");
                }
                else if (currentFriendCount == existingLines.Length)
                {
                    DeepConsole.Log("FriendSaver", $"No changes. {currentFriendCount} friendIDs already saved.");
                }
                else
                {
                    DeepConsole.Log("FriendSaver", $"File has more entries than the current friend list. No changes made.");
                }
            }
            else
            {
                File.WriteAllLines(filePath, currentFriendIDs);
                DeepConsole.Log("FriendSaver", $"File created and {currentFriendCount} friendIDs saved.");
            }
        }
        public static string GetGreeting()
        {
            int hour = DateTime.Now.Hour;
            if (hour >= 0 && hour < 12)
            {
                return "Good morning";
            }
            else if (hour >= 12 && hour < 18)
            {
                return "Good day";
            }
            else
            {
                return "Good evening";
            }
        }
    }
}
