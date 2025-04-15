using System;
using System.Collections.Generic;
using DeepCore.Client.Misc;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
    internal class LastSeenAvatars
    {
        internal static void LoggedAvatarsMenu(UiManager uiManager)
        {
            _uiManager = uiManager;
            _LastSeenAvatarsMenu = _uiManager.QMMenu.AddCategoryPage("Avatars Logger", "Avatars Logger");
            _LastSeenAvatarsMenu.OnOpen += lastSeenAction;
        }
        private static void lastSeenAction()
        {
            if (calledTwice)
            {
                calledTwice = false;
                return;
            }
            calledTwice = true;
            if (tempPage != 0)
            {
                _LastSeenAvatars.Active = false;
            }
            int num = ++tempPage;
            _LastSeenAvatarsMenu.AddCategory("Logged avi (" + num.ToString() + ")");
            _LastSeenAvatars = _LastSeenAvatarsMenu.GetCategory("Logged avi (" + num.ToString() + ")");
            List<Avatar_Object> list = AvatarLoggerHandler.Fetch();
            if (list == null || list.Count == 0)
            {
                return;
            }
            list.Reverse();
            int count = 1;
            IEnumerable<Avatar_Object> source = list;
            Func<Avatar_Object, bool> predicate;
            using (IEnumerator<Avatar_Object> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    Avatar_Object avatarObject = enumerator.Current;
                    int count2 = count;
                    count = count2 + 1;
                    if (!(avatarObject.releaseStatus != "public") && avatarObject.supportedPlatforms != 2 && !avatarObject.isblackListed)
                    {
                        string tooltip = string.Concat(new string[]
                        {
                            "Id: (",
                            avatarObject.id,
                            ") By: (",
                            avatarObject.authorName,
                            ") Description: ",
                            avatarObject.description
                        });
                        _LastSeenAvatars.AddButton(avatarObject.name, tooltip, delegate
                        {
                            VrcExtensions.ChangeAvatar(avatarObject.id);
                        }, null);
                    }
                }
            }
        }
        private static UiManager _uiManager;
        private static ReCategoryPage _LastSeenAvatarsMenu;
        private static ReMenuCategory _LastSeenAvatars;
        private static int tempPage;
        private static bool calledTwice;
    }
}
