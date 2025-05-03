
namespace AstroClient.MenuApi.Tools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AstroClient.Tools.Extensions;
    using AstroClient.xAstroBoy.Utility;
    using CheetoLibrary.Misc;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using VRC;
    using VRC.Core;
    using VRC.DataModel.Core;
    using VRC.UI;
    using VRC.UI.Core.Styles;
    using VRC.UI.Elements;
    using VRC.UI.Elements.Analytics;
    using VRC.UI.Elements.Controls;
    using VRC.UI.Elements.Menus;
    using VRC.UI.Elements.Tooltips;
    using VRC.UI.Elements.Wings;
    using Object = Il2CppSystem.Object;

    internal static class MenuTools
    {

        public static void DestroyChildren(this Transform transform, Func<Transform, bool> exclude)
        {
            for (var childcount = transform.childCount - 1; childcount >= 0; childcount--)
                if (exclude == null || exclude(transform.GetChild(childcount)))
                    UnityEngine.Object.DestroyImmediate(transform.GetChild(childcount).gameObject);
        }

        public static void DestroyChildren(this Transform transform) =>
            transform.DestroyChildren(null);

        public static void DestroyChildren(this GameObject gameObj) =>
            gameObj.transform.DestroyChildren(null);


        internal static void ToggleScrollRectOnExistingMenu(this GameObject NestedPart, bool active)
        {
            try
            {
                var scrollRect = NestedPart.GetComponentInChildren<ScrollRect>(true);
                var scrollbar = NestedPart.GetComponentInChildren<Scrollbar>(true);
                var layout = NestedPart.GetComponentInChildren<VerticalLayoutGroup>(true);
                if (scrollbar != null && scrollRect != null)
                {
                    scrollbar.enabled = active;
                    scrollbar.gameObject.SetActive(active);
                    scrollRect.enabled = active;
                    scrollRect.gameObject.SetActive(active);

                    //var buttons = NestedPart.FindObject("Buttons");
                    //if (buttons != null)
                    //{
                    //    scrollRect.viewport = buttons.GetComponent<RectTransform>();
                    //}
                    if (layout != null) layout.childControlHeight = true;
                    scrollRect.movementType = ScrollRect.MovementType.Elastic;
                    scrollRect.verticalScrollbar = scrollbar;
                    //scrollRect.horizontalScrollbar = scrollbar;
                }
            }
            catch (Exception e)
            {
                Log.Exception(e);
            }
        }

        public static IUser ToIUser(this APIUser value)
        {
            return ((Object)UiMethods._apiUserToIUser.Invoke(DataModelManager.field_Private_Static_DataModelManager_0.field_Private_ObjectPublicDi2StObUnique_0, new object[3] { value.id, value, false })).Cast<IUser>();
            // return ((Object)UiMethods._apiUserToIUser.Invoke(DataModelManager.field_Private_Static_DataModelManager_0.field_Private_DataModelCache_0, new object[3] { value.id, value, false })).Cast<IUser>();

        }

        /// <summary>
        ///     Converts the given IUser to an APIUser.
        /// </summary>
        /// <param name="value">The IUser to convert to APIUser</param>
        /// <returns></returns>
        public static APIUser ToAPIUser(this IUser value)
        {
            return value.Cast<APIUser>();
        }

        private static Transform _interface;

        internal static PlayerManager PlayerManager => PlayerManager.prop_PlayerManager_0;
        internal static List<Player> CurrentPlayers => PlayerManager.field_Private_List_1_Player_0.ToArray().ToList();
        public static Player SelectedPlayer
        {
            get
            {
                return GetPlayerByUserID(ApiPaths.SelectedUserMenuQM.prop_String_0);
            }
        }
        internal static Player GetPlayerByUserID(string id)
        {
            foreach (var player in CurrentPlayers.Where(player => player.GetAPIUser().GetUserID() == id))
            {
                return player;
            }

            return null;
        }


        internal static string GetName(this UIPage page)
        {
            return page.field_Public_String_0;
        }
        internal static void SetName(this UIPage page, string Name)
        {
            if (page != null)
            {
                page.name = Name;
                page.field_Public_String_0 = Name;
            }
        }

        internal static GameObject FindUIObject(this GameObject parent, string name)
        {
            if (parent == null) return null;
            Transform[] trs = parent.GetComponentsInChildren<Transform>(true);
            foreach (var t in trs)
                if (t.name == name)
                    return t.gameObject;
            return null;
        }

        internal static bool IsObfuscated(this string str)
        {
            foreach (var it in str)
                if (!char.IsDigit(it) && !((it >= 'a' && it <= 'z') || (it >= 'A' && it <= 'Z')) && it != '_' &&
                    it != '`' && it != '.' && it != '<' && it != '>')
                    return true;

            return false;
        }
        public static void EnableUIComponents(this GameObject parent)
        {
            if (parent == null) return;                                           // <-- added

            //parent.RemoveComponents<MonoBehaviourPublic16Bu47Vo4947VoSt49VoUnique>();
            parent.RemoveComponents<CameraMenu>();
            parent.RemoveComponents<AnalyticsController>();
            parent.RemoveComponents<AvatarClone>();
            parent.RemoveComponents<MenuBackButton>();
            parent.RemoveComponents<WingMenuFriends>();
            parent.RemoveComponents<UiToggleTooltip>();

            #region Button
            var Button = parent.GetComponent<Button>();
            if (Button != null) Button.enabled = true;

            var Buttons_List = parent.GetComponentsInChildren<Button>(true);
            for (var i = 0; i < Buttons_List.Count; i++)
            {
                var item = Buttons_List[i];
                if (item != null) item.enabled = true;
            }
            #endregion

            #region LayoutElement
            var LayoutElement = parent.GetComponent<LayoutElement>();
            if (LayoutElement != null) LayoutElement.enabled = true;

            var LayoutElements_List = parent.GetComponentsInChildren<LayoutElement>(true);
            for (var i = 0; i < LayoutElements_List.Count; i++)
            {
                var item = LayoutElements_List[i];
                if (item != null) item.enabled = true;
            }
            #endregion

            #region CanvasGroup
            var CanvasGroup = parent.GetComponent<CanvasGroup>();
            if (CanvasGroup != null) CanvasGroup.enabled = true;

            var CanvasGroups_List = parent.GetComponentsInChildren<CanvasGroup>(true);
            for (var i = 0; i < CanvasGroups_List.Count; i++)
            {
                var item = CanvasGroups_List[i];
                if (item != null) item.enabled = true;
            }
            #endregion

            #region StyleElement
            var StyleElement = parent.GetComponent<StyleElement>();
            if (StyleElement != null) StyleElement.enabled = true;

            var StyleElements_List = parent.GetComponentsInChildren<StyleElement>(true);
            for (var i = 0; i < StyleElements_List.Count; i++)
            {
                var item = StyleElements_List[i];
                if (item != null) item.enabled = true;
            }
            #endregion

            #region Image
            var Image = parent.GetComponent<Image>();
            if (Image != null) Image.enabled = true;

            var Images_List = parent.GetComponentsInChildren<Image>(true);
            for (var i = 0; i < Images_List.Count; i++)
            {
                var item = Images_List[i];
                if (item != null) item.enabled = true;
            }
            #endregion

            #region TextMeshProUGUIEx
            var TextMeshProUGUIPublicBoUnique = parent.GetComponent<TextMeshProUGUIEx>();
            if (TextMeshProUGUIPublicBoUnique != null) TextMeshProUGUIPublicBoUnique.enabled = true;

            var TextMeshProUGUIPublicBoUnique_List = parent.GetComponentsInChildren<TextMeshProUGUIEx>(true);
            for (var i = 0; i < TextMeshProUGUIPublicBoUnique_List.Count; i++)
            {
                var item = TextMeshProUGUIPublicBoUnique_List[i];
                if (item != null) item.enabled = true;
            }
            #endregion

            #region TextMeshProUGUI
            var TextMeshProUGUI = parent.GetComponent<TextMeshProUGUI>();
            if (TextMeshProUGUI != null) TextMeshProUGUI.enabled = true;

            var TextMeshProUGUIs_List = parent.GetComponentsInChildren<TextMeshProUGUI>(true);
            for (var i = 0; i < TextMeshProUGUIs_List.Count; i++)
            {
                var item = TextMeshProUGUIs_List[i];
                if (item != null) item.enabled = true;
            }
            #endregion

            #region ToolTip
            var ToolTip = parent.GetComponent<ToolTip>();
            if (ToolTip != null) ToolTip.enabled = true;

            var ToolTips_List = parent.GetComponentsInChildren<ToolTip>(true);
            for (var i = 0; i < ToolTips_List.Count; i++)
            {
                var item = ToolTips_List[i];
                if (item != null) item.enabled = true;
            }
            #endregion

            #region Toggle
            var Toggle = parent.GetComponent<Toggle>();
            if (Toggle != null) Toggle.enabled = true;

            var Toggles_List = parent.GetComponentsInChildren<Toggle>(true);
            for (var i = 0; i < Toggles_List.Count; i++)
            {
                var item = Toggles_List[i];
                if (item != null) item.enabled = true;
            }
            #endregion

            #region UIInvisibleGraphic
            var UIInvisibleGraphic = parent.GetComponent<UIInvisibleGraphic>();
            if (UIInvisibleGraphic != null) UIInvisibleGraphic.enabled = true;

            var UIInvisibleGraphics_List = parent.GetComponentsInChildren<UIInvisibleGraphic>(true);
            for (var i = 0; i < UIInvisibleGraphics_List.Count; i++)
            {
                var item = UIInvisibleGraphics_List[i];
                if (item != null) item.enabled = true;
            }
            #endregion

            foreach (var item in parent.GetComponentsInChildren<Behaviour>(true))
            {
                if (item != null) item.enabled = true;                           // <-- added
            }
        }

        public static TextMeshProUGUIEx NewText(this GameObject parent, string search)
        {
            var texts = parent.GetComponentsInChildren<TextMeshProUGUIEx>(true);
            var match = texts
                .FirstOrDefault(t => string.Equals(t.transform.name, search, StringComparison.OrdinalIgnoreCase));

            if (match != null)
                return match;

            var names = string.Join(", ", texts.Select(t => t.transform.name));
            Log.Debug($"Text not found: {search} in {parent.name} ({parent.GetPath()})\nNames: {names}");

            return null;
        }


        public static void CleanButtonsQuickActions(this GameObject Parent)
        {
            var Buttons = Parent.GetComponentsInChildren<Transform>(true);
            for (int i = 0; i < Buttons.Count; i++)
            {
                Transform button = Buttons[i];
                if (button.name.Contains("Button_") || button.name == "SitStandCalibrateButton" || button.name == "Buttons_AvatarDetails"
                    || button.name == "Buttons_AvatarAuthor")
                    UnityEngine.Object.Destroy(button.gameObject);
            }
        }

        public static void CleanCameraMenu(this GameObject Parent)
        {
            foreach (var child in Parent.Get_Childs())
            {
                if (child.gameObject.name != "Buttons (1)")
                {
                    UnityEngine.Object.Destroy(child.gameObject);
                }
                else
                {
                    foreach (var child2 in child.Get_Childs())
                    {
                        UnityEngine.Object.Destroy(child2.gameObject);
                    }
                }
            }
        }



        internal static void ShowTabContent(this MenuStateController MenuController, string PageName)
        {
            if (MenuController != null)
            {
                UIPage[] Pages = MenuController.field_Public_ArrayOf_UIPage_0;
                if (Pages != null)
                {
                    if (Pages.Length != 0)
                    {
                        for (int i = 0; i < Pages.Length; i++)
                        {
                            var page = Pages[i];
                            if (page != null)
                            {
                                if (page.GetName() == PageName)
                                {
                                    MenuController.ShowTabContent(i);
                                    break;
                                }
                            }

                        }
                    }
                }
            }
        }

        //internal static void ShowWingsPage(this QMWings pagename)
        //{
        //    if (pagename.isLeftWing)
        //    {
        //        QuickMenuTools.QM_Wing_Left.ShowTabContent(pagename.GetMenuName());
        //    }
        //    else
        //    {
        //        QuickMenuTools.QM_Wing_Right.ShowTabContent(pagename.GetMenuName());
        //    }
        //}




        internal static Color HexToColor(this string hex)
        {
            var color = Color.white;

            if (ColorUtility.TryParseHtmlString(hex, out color)) return color;

            return color;
        }


    }
}