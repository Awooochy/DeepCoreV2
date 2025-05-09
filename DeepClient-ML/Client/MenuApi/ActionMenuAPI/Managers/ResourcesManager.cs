namespace AstroClient.MenuApi.ActionMenuAPI.Managers
{
    using AstroClient.ClientActions;
    using AstroClient.ClientResources.Loaders;
    using Helpers;
    using UnityEngine;
    using UnityEngine.UI;

    internal class ResourceManagerEvents : AstroEvents
    {
        internal override void RegisterToEvents()
        {
            ClientEventActions.VRChat_OnActionMenuInit += VRChat_OnActionMenuInit;

        }
        private void VRChat_OnActionMenuInit()
        {
            ResourcesManager.InitLockGameObject();
        }
    }

    internal static class ResourcesManager
    {
        private static GameObject lockPrefab;
        public static void InitLockGameObject()
        {
            lockPrefab = Object.Instantiate(ActionMenuController.field_Public_Static_ActionMenuController_0.GetRightOpener().GetActionMenu()
                .GetPedalOptionPrefab().GetComponent<PedalOption>().GetActionButton().gameObject.GetChild("Inner")
                .GetChild("Folder Icon"));
            Object.DontDestroyOnLoad(lockPrefab);
            lockPrefab.active = false;
            lockPrefab.gameObject.name = Constants.LOCKED_PEDAL_OVERLAY_GAMEOBJECT_NAME;
            lockPrefab.GetComponent<RawImage>().texture = Icons.locked;
            Log.Debug("Created lock gameobject");
        }

        public static Texture2D GetPageIcon(int pageIndex)
        {
            switch (pageIndex)
            {
                case 1:
                    return Icons.one;

                case 2:
                    return Icons.two;

                case 3:
                    return Icons.three;

                case 4:
                    return Icons.four;

                case 5:
                    return Icons.five;

                case 6:
                    return Icons.six;

                case 7:
                    return Icons.seven;

                default:
                    return null;
            }
        }

        public static void AddLockChildIcon(GameObject parent)
        {
            var lockedGameObject = Object.Instantiate(lockPrefab, parent.transform, false);
            lockedGameObject.SetActive(true);
            lockedGameObject.transform.localPosition = new Vector3(50, -25, 0);
            lockedGameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }

        public static Texture2D GetModsSectionIcon()
        {
            return Icons.planet;
        }
    }
}