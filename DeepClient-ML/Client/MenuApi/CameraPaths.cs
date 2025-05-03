namespace AstroClient.MenuApi
{
    #region Usings

    using UnityEngine;

    #endregion

    internal class CameraPaths
    {
        internal static Transform Path_MainCamera
        {
            get
            {
                if (_UserCamera == null)
                {
                    var camerapath1 = ApiPaths.Application.FindObject("TrackingVolume/TrackingSteam2(Clone)/SteamCamera/[CameraRig]/Neck/Camera").transform;
                    if (camerapath1 != null)
                    {
                        return _UserCamera = camerapath1;
                    }

                    return null;
                } 
                return _UserCamera;
                
            }
        }

        internal static Camera _MainCamera;

        internal static Camera MainCamera
        {
            get
            {
                if (_MainCamera == null)
                {
                    _MainCamera = Path_MainCamera.GetComponent<Camera>();
                    // if fails, try another way
                    var FX = Resources.FindObjectsOfTypeAll<HighlightsFXStandalone>();
                    if (FX.Length > 0)
                    {
                        foreach (var fx in FX)
                        {
                            if (fx != null && fx.gameObject != null)
                            {
                                _MainCamera = fx.GetComponent<Camera>();
                                break;
                            }
                        }
                    }
                }

                return _MainCamera;
            }
        }

        internal static Transform PathPathUserCamera2
        {
            get
            {
                if (_Path_UserCamera2 == null)
                {
                    var camerapath2 = ApiPaths.Application
                        .FindObject("TrackingVolume/PlayerObjects/InverseScaleRoot/UserCamera").transform;
                    if (camerapath2 != null)
                    {
                        return _UserCamera = camerapath2;
                    }

                    return null;
                }

                return _Path_UserCamera2;

            }
        }


        internal static Camera _UserCamera2;

        internal static Camera UserCamera2
        {
            get
            {
                if (_UserCamera2 == null)
                {
                    _UserCamera2 = PathPathUserCamera2.GetComponent<Camera>();
                }
                return _UserCamera2;
            }
        }


        internal static Transform ViewFinder
        {
            get
            {
                if (_ViewFinder != null)
                {
                    return _ViewFinder;
                }
                else
                {
                    var item = PathPathUserCamera2.FindObject("ViewFinder").transform;
                    if (item != null)
                    {
                        return _ViewFinder = item;
                    }
                }
                return null;
            }
        }


        private static Transform _UserCamera;
        private static Transform _Path_UserCamera2;
        private static Transform _ViewFinder;
    }
}