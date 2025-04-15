using DeepCore.Client.Misc;
using MelonLoader;
using VRC.Core;
using VRC.SDKBase;

namespace DeepCore.Client.Module.QOL
{
    internal class LastInstanceRejoiner
    {
        public static void Rejoin()
        {
            Networking.GoToRoom(ConfManager.LastInstanceID.Value);  
        }
        public static void SaveInstanceID()
        {
            if (ConfManager.JoinLastInstance.Value)
            {
                ConfManager.LastInstanceID.Value = APIUser.CurrentUser.location;
                SaveCurrentInctance();
                MelonPreferences.Save();
            }
        }
        public static void SaveCurrentInctance()
        {
            if (ConfManager.JoinLastInstance.Value)
            {
                WorldLoggerHandler.Log();
            }
        }
    }
}
