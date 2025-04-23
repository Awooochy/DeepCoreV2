using System;
using System.Net;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
    internal class LovenseFunctions
    {
        public static void LovenseFunctionsMenu(UiManager UIManager)
        {
            ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("Lovense Functions", "Open the Lovense Functions menu", null, "#fc03e8");
            reCategoryPage.AddCategory("Functions");
            ReMenuCategory category = reCategoryPage.GetCategory("Functions");
            category.AddButton("Try Connect", "Try Connect.", async delegate ()
            {
                string lovenseHost = "http://127.0.0.1:30010";
                Console.WriteLine("Checking Lovense connection...");
                if (!CheckConnection(lovenseHost))
                {
                    Console.WriteLine("Lovense Connect API is not available. Please ensure Lovense Connect is running.");
                    return;
                }

                Console.WriteLine("Fetching connected devices...");
                string deviceId = GetDeviceId(lovenseHost);
                if (string.IsNullOrEmpty(deviceId))
                {
                    Console.WriteLine("No Lovense devices found.");
                    return;
                }
                Console.WriteLine("Device found: " + deviceId);
                Console.WriteLine("AVER COJAN");
                Console.WriteLine("Sending vibration command...");
                SendVibrationCommand(lovenseHost, deviceId, 5);
            });
        }

        static bool CheckConnection(string baseUrl)
        {
            try
            {
                WebClient client = new WebClient();
                client.DownloadString(baseUrl + "/GetToys");
                return true;
            }
            catch
            {
                return false;
            }
        }

        static string GetDeviceId(string baseUrl)
        {
            try
            {
                WebClient client = new WebClient();
                string response = client.DownloadString(baseUrl + "/GetToys");
                return "a";
            }
            catch
            {
                return null;
            }
        }

        static void SendVibrationCommand(string baseUrl, string deviceId, int level)
        {
            try
            {
                WebClient client = new WebClient();
                string commandUrl = $"{baseUrl}/Vibrate?v={level}&t={deviceId}";
                client.DownloadString(commandUrl);
                Console.WriteLine("Vibration command sent successfully!");
            }
            catch
            {
                Console.WriteLine("Failed to send vibration command.");
            }
        }
    }
}