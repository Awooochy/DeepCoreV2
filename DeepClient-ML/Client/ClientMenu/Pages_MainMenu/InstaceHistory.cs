using System;
using System.Collections.Generic;
using DeepCore.Client.Misc;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using VRC.SDKBase;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
    internal class InstaceHistory
    {
        internal static void InstaceHistoryHacksMenu(UiManager uiManager)
        {
            _uiManager = uiManager;
            _InstanceHistoryMenu = _uiManager.QMMenu.AddCategoryPage("Instance History", "Instance History");
            _InstanceHistoryMenu.OnOpen += instanceAction;
        }
        private static void instanceAction()
        {
            if (tempPage != 0)
            {
                _InstanceHistory.Active = false;
            }
            int num = ++tempPage;
            _InstanceHistoryMenu.AddCategory("World History (" + num.ToString() + ")");
            _InstanceHistory = _InstanceHistoryMenu.GetCategory("World History (" + num.ToString() + ")");
            if (WorldLoggerHandler.Fetch() == null && WorldLoggerHandler.Fetch().Count > 0)
            {
                return;
            }
            List<World_Object> list = WorldLoggerHandler.Fetch();
            list.Reverse();
            int count = 1;
            IEnumerable<World_Object> source = list;
            Func<World_Object, bool> predicate;
            using (IEnumerator<World_Object> enumerator = source.GetEnumerator())
            {
                while (enumerator.MoveNext())
                {
                    World_Object world_object = enumerator.Current;
                    int count2 = count;
                    count = count2 + 1;
                    _InstanceHistory.AddButton(world_object.worldName, string.Concat(new string[]
                    {
                        world_object.worldName,
                        " - ",
                        world_object.worldID,
                        ":",
                        world_object.instanceName,
                        " Type:(",
                        world_object.instanceType,
                        ")."
                    }), delegate
                    {
                        Networking.GoToRoom(world_object.instanceID);
                    }, null);
                }
            }
        }
        private static UiManager _uiManager;
        private static ReCategoryPage _InstanceHistoryMenu;
        private static ReMenuCategory _InstanceHistory;
        private static int tempPage;
    }
}
