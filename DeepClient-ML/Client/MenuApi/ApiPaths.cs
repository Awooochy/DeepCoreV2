namespace AstroClient.MenuApi
{
    using Tools;
    using UnityEngine;
    using UnityEngine.UI;
    using VRC.UI.Elements;
    using VRC.UI.Elements.Controls;
    using VRC.UI.Elements.Menus;

    internal static class ApiPaths
    {
        internal static bool DONT_WARN = true; 

        internal static Transform VRCUIManager => VRCUiManager.field_Private_Static_VRCUiManager_0.transform;






        internal static Transform _UserInterface;

        public static Transform UserInterface
        {
            get
            {
                if (_UserInterface == null)
                    return _UserInterface = Resources.FindObjectsOfTypeAll<UserInterface>()[0].gameObject.transform;
                return _UserInterface;
            }
        }
        private static Transform _Application;
        internal static Transform Application
        {
            get
            {
                if (_Application == null)
                    return _Application = Resources.FindObjectsOfTypeAll<VRCApplication>()[0].gameObject.transform;
                return _Application;
            }
        }



        private static GameObject _Canvas_QuickMenu;

        public static GameObject Canvas_QuickMenu
        {
            get
            {
                if (_Canvas_QuickMenu == null)
                {
                    _Canvas_QuickMenu = VRCUIManager.FindObject("Canvas_QuickMenu(Clone)", DONT_WARN).gameObject;
                }

                return _Canvas_QuickMenu;
            }
        }

        internal static QuickMenu _QuickMenu;

        public static QuickMenu QuickMenu
        {
            get
            {
                if (_QuickMenu == null)
                    return _QuickMenu = Canvas_QuickMenu.GetComponent<QuickMenu>();
                return _QuickMenu;
            }
        }



        private static GameObject _Button_NewInstance;

        public static GameObject Button_NewInstance
        {
            get
            {
                if (_Button_NewInstance == null)
                {
                    _Button_NewInstance = Canvas_QuickMenu.FindObject(
                        "CanvasGroup/Container/Window/QMParent/Menu_Here/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_WorldActions/Button_NewInstance" , DONT_WARN).gameObject;;
                }

                return _Button_NewInstance;
            }
        }


        private static GameObject _Menu_DashBoard;

        public static GameObject Menu_DashBoard
        {
            get
            {
                if (_Menu_DashBoard == null)
                {
                    _Menu_DashBoard = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_Dashboard" , DONT_WARN).gameObject;;
                }

                return _Menu_DashBoard;
            }
        }

        private static GameObject _Menu_Camera;

        public static GameObject Menu_Camera
        {
            get
            {
                if (_Menu_Camera == null)
                {
                    _Menu_Camera = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_Camera" , DONT_WARN).gameObject;;
                }

                return _Menu_Camera;
            }
        }

        private static GameObject _Tab;

        public static GameObject Tab
        {
            get
            {
                if (_Tab == null)
                {
                    _Tab = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_DevTools" , DONT_WARN).gameObject;;
                }
                return _Tab;
            }
        }

        private static GameObject _ButtonGrp;

        public static GameObject ButtonGrp
        {
            get
            {
                if (_ButtonGrp == null)
                {
                    _ButtonGrp = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_Camera/Scrollrect/Viewport/VerticalLayoutGroup/Buttons" , DONT_WARN).gameObject;;
                }
                return _ButtonGrp;
            }
        }

        private static GameObject _ButtonGrpText;

        public static GameObject ButtonGrpText
        {
            get
            {
                if (_ButtonGrpText == null)
                {
                    _ButtonGrpText = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_Camera/Scrollrect/Viewport/VerticalLayoutGroup/Header_H3" , DONT_WARN).gameObject;;
                }
                return _ButtonGrpText;
            }
        }

        private static GameObject _ColpButtonGrp;

        public static GameObject ColpButtonGrp
        {
            get
            {
                if (_ColpButtonGrp == null)
                {
                    _ColpButtonGrp = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/YourAvatar" , DONT_WARN).gameObject;;
                }
                return _ColpButtonGrp;
            }
        }

        private static GameObject _Slider;

        public static GameObject Slider
        {
            get
            {
                if (_Slider == null)
                {
                    _Slider = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/DisplayAndVisualAdjustments/QM_Settings_Panel/VerticalLayoutGroup/ScreenBrightness" , DONT_WARN).gameObject;;
                }
                return _Slider;
            }
        }

        private static GameObject _TriToggle;

        public static GameObject TriToggle
        {
            get
            {
                if (_TriToggle == null)
                {
                    _TriToggle = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local/ScrollRect/Viewport/VerticalLayoutGroup/Buttons_AvatarActions/Button_AvatarControls" , DONT_WARN).gameObject;;
                }
                return _TriToggle;
            }
        }

        private static GameObject _QMCarouselPageTemplate;

        public static GameObject QMCarouselPageTemplate
        {
            get
            {
                if (_QMCarouselPageTemplate == null)
                {
                    _QMCarouselPageTemplate = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/UIElements" , DONT_WARN).gameObject;;
                }
                return _QMCarouselPageTemplate;
            }
        }

        private static GameObject _QMCarouselSeparator;

        public static GameObject QMCarouselSeparator
        {
            get
            {
                if (_QMCarouselSeparator == null)
                {
                    _QMCarouselSeparator = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/UIElements/QM_Settings_Panel/VerticalLayoutGroup/Separator" , DONT_WARN).gameObject;;
                }
                return _QMCarouselSeparator;
            }
        }

        private static GameObject _QMCarouselToggleTemplate;

        public static GameObject QMCarouselToggleTemplate
        {
            get
            {
                if (_QMCarouselToggleTemplate == null)
                {
                    _QMCarouselToggleTemplate = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/UIElements/QM_Settings_Panel/VerticalLayoutGroup/ShowGroupBanner" , DONT_WARN).gameObject;;
                }
                return _QMCarouselToggleTemplate;
            }
        }

        private static GameObject _QMCarouselSelectorTemplate;

        public static GameObject QMCarouselSelectorTemplate
        {
            get
            {
                if (_QMCarouselSelectorTemplate == null)
                {
                    _QMCarouselSelectorTemplate = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/UIElements/QM_Settings_Panel/VerticalLayoutGroup/NameplateVisibility" , DONT_WARN).gameObject;;
                }
                return _QMCarouselSelectorTemplate;
            }
        }

        private static GameObject _QMCarouselTitleTemplate;

        public static GameObject QMCarouselTitleTemplate
        {
            get
            {
                if (_QMCarouselTitleTemplate == null)
                {
                    _QMCarouselTitleTemplate = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Chatbox/QM_Settings_Panel/VerticalLayoutGroup/ChatboxFunctionTitle" , DONT_WARN).gameObject;;
                }
                return _QMCarouselTitleTemplate;
            }
        }

        private static GameObject _QMCarouselFuncButtonTemplate;

        public static GameObject QMCarouselFuncButtonTemplate
        {
            get
            {
                if (_QMCarouselFuncButtonTemplate == null)
                {
                    _QMCarouselFuncButtonTemplate = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_QM_GeneralSettings/Panel_QM_ScrollRect/Viewport/VerticalLayoutGroup/Chatbox/QM_Settings_Panel/VerticalLayoutGroup/ChatboxFunctionButtons" , DONT_WARN).gameObject;;
                }
                return _QMCarouselFuncButtonTemplate;
            }
        }

        private static GameObject _WingParentL;

        public static GameObject WingParentL
        {
            get
            {
                if (_WingParentL == null)
                {
                    _WingParentL = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/Wing_Left/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup" , DONT_WARN).gameObject;;
                }
                return _WingParentL;
            }
        }

        private static GameObject _WingParentR;

        public static GameObject WingParentR
        {
            get
            {
                if (_WingParentR == null)
                {
                    _WingParentR = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/WingMenu/ScrollRect/Viewport/VerticalLayoutGroup" , DONT_WARN).gameObject;;
                }
                return _WingParentR;
            }
        }



        private static GameObject _WingPage_Left;

        public static GameObject WingPage_Left
        {
            get
            {
                if (_WingPage_Left == null)
                {
                    _WingPage_Left = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/Wing_Left/Container/InnerContainer/Profile" , DONT_WARN).gameObject;
                }
                return _WingPage_Left;
            }
        }

        private static GameObject _WingPage_Right;

        public static GameObject WingPage_Right
        {
            get
            {
                if (_WingPage_Right == null)
                {
                    _WingPage_Right = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/Wing_Right/Container/InnerContainer/Profile" , DONT_WARN).gameObject;
                }
                return _WingPage_Right;
            }
        }

        private static UIPage _WingPageComponent_Left;
        public static UIPage WingPageComponent_Left
        {
            get
            {
                if (_WingPageComponent_Left == null)
                {
                    _WingPageComponent_Left = WingPage_Left.GetComponent<UIPage>();
                }

                return _WingPageComponent_Left;
            }
        }

        private static UIPage _WingPageComponent_Right;

        public static UIPage WingPageComponent_Right
        {
            get
            {
                if (_WingPageComponent_Right == null)
                {
                    _WingPageComponent_Right = WingPage_Right.GetComponent<UIPage>();
                }

                return _WingPageComponent_Right;
            }
        }


  

        private static GameObject _Canvas_MainMenu;

        public static GameObject Canvas_MainMenu
        {
            get
            {
                if (_Canvas_MainMenu == null)
                {
                    _Canvas_MainMenu = VRCUIManager.FindObject("Canvas_MainMenu(Clone)", DONT_WARN).gameObject;
                }
                return _Canvas_MainMenu;
            }
        }

        private static GameObject _MMMpageTemplate;

        public static GameObject MMMpageTemplate
        {
            get
            {
                if (_MMMpageTemplate == null)
                {
                    _MMMpageTemplate = Canvas_MainMenu.FindObject("Container/MMParent/HeaderOffset/Menu_MM_Profile" , DONT_WARN).gameObject;;
                }
                return _MMMpageTemplate;
            }
        }

        private static GameObject _MMMCarouselPageTemplate;

        public static GameObject MMMCarouselPageTemplate
        {
            get
            {
                if (_MMMCarouselPageTemplate == null)
                {
                    _MMMCarouselPageTemplate = Canvas_MainMenu.FindObject("Container/MMParent/HeaderOffset/Menu_Settings" , DONT_WARN).gameObject;;
                }
                return _MMMCarouselPageTemplate;
            }
        }

        private static GameObject _MMMTabTemplate;

        public static GameObject MMMTabTemplate
        {
            get
            {
                if (_MMMTabTemplate == null)
                {
                    _MMMTabTemplate = Canvas_MainMenu.FindObject("Container/PageButtons/HorizontalLayoutGroup/Launch_Pad_Button_Tab" , DONT_WARN).gameObject;;
                }
                return _MMMTabTemplate;
            }
        }

        private static GameObject _MMMCarouselButtonTemplate;

        public static GameObject MMMCarouselButtonTemplate
        {
            get
            {
                if (_MMMCarouselButtonTemplate == null)
                {
                    _MMMCarouselButtonTemplate = Canvas_MainMenu.FindObject("Container/MMParent/HeaderOffset/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Navigation/Viewport/VerticalLayoutGroup/Cell_MM_Audio & Voice" , DONT_WARN).gameObject;;
                }
                return _MMMCarouselButtonTemplate;
            }
        }

        private static GameObject _MMBtnGRP;

        public static GameObject MMBtnGRP
        {
            get
            {
                if (_MMBtnGRP == null)
                {
                    _MMBtnGRP = Canvas_MainMenu.FindObject("Container/MMParent/HeaderOffset/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Debug/ManageCachedData/" , DONT_WARN).gameObject;;
                }
                return _MMBtnGRP;
            }
        }

        private static GameObject _MMCTgl;

        public static GameObject MMCTgl
        {
            get
            {
                if (_MMCTgl == null)
                {
                    _MMCTgl = Canvas_MainMenu.FindObject("Container/MMParent/HeaderOffset/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/Mirrors/PersonalMirror/Settings_Panel_1/VerticalLayoutGroup/PersonalMirror" , DONT_WARN).gameObject;;
                }
                return _MMCTgl;
            }
        }

        private static GameObject _MMMSldr;

        public static GameObject MMMSldr
        {
            get
            {
                if (_MMMSldr == null)
                {
                    _MMMSldr = Canvas_MainMenu.FindObject("Container/MMParent/HeaderOffset/Menu_Settings/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation_Container/ScrollRect_Content/Viewport/VerticalLayoutGroup/AudioAndVoice/AudioVolume/Settings_Panel_1/VerticalLayoutGroup/MasterVolume" , DONT_WARN).gameObject;;
                }
                return _MMMSldr;
            }
        }


        private static UIPage _qmDashboard;

        public static UIPage qmDashboard
        {
            get
            {
                if (_qmDashboard == null)
                {
                    return _qmDashboard = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_Dashboard" , DONT_WARN).gameObject.GetComponent<UIPage>();
                }

                return _qmDashboard;
            }
        }
        private static UIPage _selectedUserLocal;

        public static UIPage selectedUserLocal
        {
            get
            {
                if (_selectedUserLocal == null)
                {
                    return _selectedUserLocal = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Local" , DONT_WARN).gameObject.GetComponent<UIPage>();
                }

                return _selectedUserLocal;
            }
        }
        private static UIPage _selectedUserRemote;

        public static UIPage selectedUserRemote
        {
            get
            {
                if (_selectedUserRemote == null)
                {
                    return _selectedUserRemote = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/QMParent/Menu_SelectedUser_Remote" , DONT_WARN).gameObject.GetComponent<UIPage>();
                }

                return _selectedUserRemote;
            }
        }



        private static GameObject _WingLeft;

        public static GameObject WingLeft
        {
            get
            {
                if (_WingLeft == null)
                {
                    return _WingLeft = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/Wing_Left", DONT_WARN).gameObject;
                }

                return _WingLeft;
            }
        }



        private static GameObject _WingRight;

        public static GameObject WingRight
        {
            get
            {
                if (_WingRight == null)
                {
                    return _WingRight = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/Wing_Left", DONT_WARN)
                        .gameObject;
                }

                return _WingRight;
            }
        }

        private static GameObject _WingLeftButton;
        public static GameObject WingLeftButton
        {
            get
            {
                if (_WingLeftButton == null)
                {
                    return _WingLeftButton = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/Wing_Left/Button", DONT_WARN).gameObject;
                }
                return _WingLeftButton;
            }
        }

        private static GameObject _WingRightButton;
        public static GameObject WingRightButton
        {
            get
            {
                if (_WingRightButton == null)
                {
                    return _WingRightButton = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/Wing_Right/Button", DONT_WARN).gameObject;
                }
                return _WingRightButton;
            }
        }



        private static Sprite _checkSprite;

        public static Sprite checkSprite
        {
            get
            {
                if (_checkSprite == null)
                {
                    return _checkSprite = Canvas_QuickMenu.FindObject( "CanvasGroup/Container/Window/QMParent/Modal_AddWorldToPlaylist/MenuPanel/ScrollRect/Viewport/VerticalLayoutGroup/Cell_QM_WorldPlaylistToggle 1/ButtonElement_CheckBox/Checkmark" , DONT_WARN).GetComponent<Image>().activeSprite;
                }

                return _checkSprite;
            }
        }


        private static GameObject _UserInterface_Popups;
        public static GameObject UserInterface_Popups
        {
            get
            {
                if (_UserInterface_Popups == null)
                {
                    _UserInterface_Popups = VRCUIManager.FindObject("MenuContent/Popups", DONT_WARN).gameObject;
                }
                return _UserInterface_Popups;
            }
        }

        private static GameObject _UserInterface_LoadingPopup;
        public static GameObject UserInterface_LoadingPopup
        {
            get            
            {
                if (_UserInterface_LoadingPopup == null)
                {
                    _UserInterface_LoadingPopup = UserInterface_Popups.FindObject("LoadingPopup", DONT_WARN).gameObject;
                }
                return _UserInterface_LoadingPopup;
            }
        }

        private static GameObject _UserInterface_LoadingBackground;
        public static GameObject UserInterface_LoadingBackground
        {
            get
            {
                if (_UserInterface_LoadingBackground == null)
                {
                    _UserInterface_LoadingBackground = VRCUIManager.FindObject("LoadingBackground_TealGradient_Music", DONT_WARN).gameObject;
                }
                return _UserInterface_LoadingBackground;
            }
        }

        private static MenuStateController _QuickMenu_MenuController;
        public static MenuStateController QuickMenu_MenuController
        {
            get
            {
                if (_QuickMenu_MenuController == null)
                {
                    _QuickMenu_MenuController = Canvas_QuickMenu.GetComponent<MenuStateController>();
                }

                return _QuickMenu_MenuController;
            }
        }



        private static MenuStateController _Wing_Left_MenuController;
        public static MenuStateController Wing_Left_MenuController
        {
            get
            {
                if (_Wing_Left_MenuController == null)
                {
                    _Wing_Left_MenuController = WingLeft
                        .GetComponent<MenuStateController>();
                }

                return _Wing_Left_MenuController;
            }
        }

        private static MenuStateController _Wing_Right_MenuController;
        public static MenuStateController Wing_Right_MenuController
        {
            get
            {
                if (_Wing_Right_MenuController == null)
                {
                    _Wing_Right_MenuController = WingRight
                        .GetComponent<MenuStateController>();
                }
                return _Wing_Right_MenuController;
            }
        }

        private static GameObject _WingPageButtonTemplate;
        internal static GameObject WingPageButtonTemplate
        {
            get
            {
                if (_WingPageButtonTemplate == null)
                {
                    var Buttons = Canvas_QuickMenu.GetComponentsInChildren<Button>(true);
                    for (int i = 0; i < Buttons.Count; i++)
                    {
                        Button button = Buttons[i];
                        if (button.name == "Button_ActionMenu")
                        {
                            _WingPageButtonTemplate = button.gameObject;
                            break;
                        }
                    }
                }

                return _WingPageButtonTemplate;
            }
        }

        private static GameObject _WingButtonTemplate_Right;

        internal static GameObject WingButtonTemplate_Right
        {
            get
            {
                if (_WingButtonTemplate_Right == null)
                {
                    var Buttons = WingRight.GetComponentsInChildren<Button>(true);
                    for (int i = 0; i < Buttons.Count; i++)
                    {
                        Button button = Buttons[i];
                        if (button.name == "Button_Profile")
                        {
                            _WingButtonTemplate_Right = button.gameObject;
                            break;
                        }
                    }
                }

                return _WingButtonTemplate_Right;
            }
        }

        private static GameObject _WingButtonTemplate_Left;
        internal static GameObject WingButtonTemplate_Left
        {
            get
            {
                if (_WingButtonTemplate_Left == null)
                {
                    var Buttons = WingLeft.GetComponentsInChildren<Button>(true);
                    for (int i = 0; i < Buttons.Count; i++)
                    {
                        Button button = Buttons[i];
                        if (button.name == "Button_Profile")
                        {
                            _WingButtonTemplate_Left = button.gameObject;
                            break;
                        }
                    }
                }

                return _WingButtonTemplate_Left;
            }
        }

        private static UIPage _UIPage_WingRight_Template;
        internal static UIPage UIPage_WingRight_Template
        {
            get
            {
                if (_UIPage_WingRight_Template == null)
                {
                    var Buttons = WingRight.GetComponentsInChildren<UIPage>(true);
                    for (int i = 0; i < Buttons.Count; i++)
                    {
                        UIPage button = Buttons[i];
                        if (button.name == "Profile")
                        {
                            _UIPage_WingRight_Template = button;
                            break;
                        }
                    }
                }

                return _UIPage_WingRight_Template;
            }
        }
        private static UIPage _UIPage_WingLeft_Template;

        internal static UIPage UIPage_WingLeft_Template
        {
            get
            {
                if (_UIPage_WingLeft_Template == null)
                {
                    var Buttons = WingLeft.GetComponentsInChildren<UIPage>(true);
                    for (int i = 0; i < Buttons.Count; i++)
                    {
                        UIPage button = Buttons[i];
                        if (button.name == "Profile")
                        {
                            _UIPage_WingLeft_Template = button;
                            break;
                        }
                    }
                }

                return _UIPage_WingLeft_Template;
            }
        }

        private static GameObject _Tabs;
        public static GameObject Tabs
        {
            get
            {
                if (_Tabs == null)
                {
                    _Tabs = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup", DONT_WARN).gameObject;
                }
                return _Tabs;
            }
        }

        private static GameObject _ToolTipPanel;
        public static GameObject ToolTipPanel
        {
            get
            {
                if (_ToolTipPanel == null)
                {
                    _ToolTipPanel = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/ToolTipPanel", DONT_WARN).gameObject;
                }
                return _ToolTipPanel;
            }
        }

        public static GameObject _Background_QM_PagePanel;
        public static GameObject Background_QM_PagePanel
        {
            get
            {
                if (_Background_QM_PagePanel == null)
                {
                    _Background_QM_PagePanel = Canvas_QuickMenu.FindObject("CanvasGroup/Container/Window/Page_Buttons_QM/HorizontalLayoutGroup/Background_QM_PagePanel", DONT_WARN).gameObject;
                }
                return _Background_QM_PagePanel;
            }
        }

        private static Transform _SelectedUserPage_Remote;

        public static Transform SelectedUserPage_Remote
        {
            get
            {
                if (_SelectedUserPage_Remote == null)
                {
                    var Buttons = Canvas_QuickMenu.GetComponentsInChildren<Transform>(true);
                    for (int i = 0; i < Buttons.Count; i++)
                    {
                        Transform button = Buttons[i];
                        if (button.name == "Menu_SelectedUser_Remote")
                        {
                            _SelectedUserPage_Remote = button;
                            break;
                        }
                    }
                }

                return _SelectedUserPage_Remote;
            }
        }

        private static Transform _SelectedUserPage_Local;
        public static Transform SelectedUserPage_Local
        {
            get
            {
                if (_SelectedUserPage_Local == null)
                {
                    var Buttons = Canvas_QuickMenu.GetComponentsInChildren<Transform>(true);
                    for (int i = 0; i < Buttons.Count; i++)
                    {
                        Transform button = Buttons[i];
                        if (button.name == "Menu_SelectedUser_Local")
                        {
                            _SelectedUserPage_Local = button;
                            break;
                        }
                    }
                }

                return _SelectedUserPage_Local;
            }
        }

        private static Transform _SelectedUserPage_Remote_VerticalLayoutGroup;
        public static Transform SelectedUserPage_Remote_VerticalLayoutGroup
        {
            get
            {
                if (_SelectedUserPage_Remote_VerticalLayoutGroup == null)
                {
                    return _SelectedUserPage_Remote_VerticalLayoutGroup =
                        SelectedUserPage_Remote.FindObject("ScrollRect/Viewport/VerticalLayoutGroup");
                }

                return _SelectedUserPage_Remote_VerticalLayoutGroup;
            }
        }

        private static Transform _SelectedUserPage_Local_VerticalLayoutGroup;

        public static Transform SelectedUserPage_Local_VerticalLayoutGroup
        {
            get
            {
                if (_SelectedUserPage_Local_VerticalLayoutGroup == null)
                {
                    return _SelectedUserPage_Local_VerticalLayoutGroup =
                        SelectedUserPage_Local.FindObject("ScrollRect/Viewport/VerticalLayoutGroup");
                }

                return _SelectedUserPage_Local_VerticalLayoutGroup;
            }
        }

        private static Transform _UnscaledUI;
        public static Transform UnscaledUI
        {
            get
            {
                if (_UnscaledUI == null) return _UnscaledUI = UserInterface.FindObject("UnscaledUI");
                return _UnscaledUI;
            }
        }

        private static SelectedUserMenuQM _SelectedUserMenuQM;
        public static SelectedUserMenuQM SelectedUserMenuQM
        {
            get
            {
                if (_SelectedUserMenuQM == null)
                    _SelectedUserMenuQM = _Canvas_QuickMenu.gameObject.FindUIObject("Menu_SelectedUser_Local")
                        .GetComponentInChildren<SelectedUserMenuQM>();
                return _SelectedUserMenuQM;
            }
        }



    }
}