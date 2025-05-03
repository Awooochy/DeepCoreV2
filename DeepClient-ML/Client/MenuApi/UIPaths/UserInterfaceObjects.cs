namespace AstroClient.MenuApi.UIPaths
{
    using UnityEngine;
    using VRC;
    using xAstroBoy;

    internal class UserInterfaceObjects
    {




        private static Transform _ScreenFade;

        internal static Transform ScreenFade
        {
            get
            {
                if (_ScreenFade == null)
                {
                    _ScreenFade = Finder.Find("UserInterface/PlayerDisplay/BlackFade/inverted_sphere").transform;
                }

                return _ScreenFade;
            }
        }
        private static SimpleAvatarPedestal _MainAvatar_Pedestral;

        internal static SimpleAvatarPedestal MainAvatar_Pedestral
        {
            get
            {
                if (_MainAvatar_Pedestral == null)
                {
                    _MainAvatar_Pedestral = AvatarPreviewBase_MainAvatar.GetComponentInChildren<SimpleAvatarPedestal>(true);
                }

                return _MainAvatar_Pedestral;
            }
        }

        private static Transform _AvatarPreviewBase_MainAvatar;

        internal static Transform AvatarPreviewBase_MainAvatar
        {
            get
            {
                if (_AvatarPreviewBase_MainAvatar == null)
                {
                    _AvatarPreviewBase_MainAvatar = Finder.Find("Canvas_MainMenu(Clone)/Container/MMParent/Menu_Avatars/Menu_MM_DynamicSidePanel/Panel_SectionList/ScrollRect_Navigation/ScrollRect_Content/Panel_SelectedAvatar/Panel_MM_AvatarViewer/").transform;
                }

                return _AvatarPreviewBase_MainAvatar;
            }
        }


        private static Transform _BigMenuElements;

        internal static Transform BigMenuElements
        {
            get
            {
                if (_BigMenuElements == null)
                {
                    return _BigMenuElements = Finder.Find("UserInterface/MenuContent/Backdrop/Backdrop").transform;;
                }
                return _BigMenuElements;
            }
        }



    }
}