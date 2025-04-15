using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using VRC.Core;

namespace DeepCore.Client.Module.ApplicationBot
{
    internal class AppBotMenu
    {
        public static bool ServerStart = false;
        public static bool WithGUi = false;
        public static void APBFunctionsMenu(UiManager UIManager)
        {
            ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("AppBot Functions", null);
            #region Bot Controls
            reCategoryPage.AddCategory("Bot Controls");
            ReMenuCategory category1 = reCategoryPage.GetCategory("Bot Controls");
            category1.AddButton("Start bot 1", "", delegate ()
            {
                bool flag = !ServerStart;
                if (flag)
                {
                    SocketConnection.StartServer();
                    ServerStart = true;
                }
                Bot.MakeBot(8, WithGUi, false);
            }, null, "#ffffff");
            category1.AddToggle("With GUI", "With gui or not?", delegate (bool s)
            {
                WithGUi = s;
            });
            #endregion
            #region Other Controls
            reCategoryPage.AddCategory("Other Controls");
            ReMenuCategory category3 = reCategoryPage.GetCategory("Other Controls");
            category3.AddButton("Join To Me [All]", "", delegate ()
            {
                var CUL = APIUser.CurrentUser.location;
                SocketConnection.SendCommandToClients($"JoinWorld {CUL}");
            });
            category3.AddButton("Join WorldID [All]", "", delegate ()
            {
                Misc.PopupHelper.PopupCall("Join WorldID", "Enter ID", "Set", false, userInput =>
                {
                    SocketConnection.SendCommandToClients($"JoinWorld {userInput}");
                });
            });
            category3.AddButton("Set Avatar [All]", "", delegate ()
            {
                Misc.PopupHelper.PopupCall("Set Avatar", "Enter ID", "Set", false, userInput =>
                {
                    SocketConnection.SendCommandToClients($"SetAvatar {userInput}");
                });
            });
            category3.AddButton("Clone My Avatar [All]", "", delegate ()
            {
                var CUL = APIUser.CurrentUser.avatarId;
                SocketConnection.SendCommandToClients($"SetAvatar {CUL}");
            });
            category3.AddButton("Send Chat Msg [All]", "", delegate ()
            {
                Misc.PopupHelper.PopupCall("Chat Message", "Enter Text", "Send", false, userInput =>
                {
                    SocketConnection.SendCommandToClients($"SendChatMsg {userInput}");
                });
            });
            #endregion
            reCategoryPage.AddCategory("Movements Controls");
            ReMenuCategory category2 = reCategoryPage.GetCategory("Movements Controls");
            #region Server Controls
            reCategoryPage.AddCategory("Server Controls");
            ReMenuCategory category = reCategoryPage.GetCategory("Server Controls");
            category.AddButton("Start Server", "", delegate
            {
                SocketConnection.StartServer();
            });
            category.AddButton("Stop Server", "", delegate
            {
                SocketConnection.StopServer();
            });
            #endregion
        }
    }
}
