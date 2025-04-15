using DeepCore.Client.Module.Exploits;
using ReMod.Core.UI.MainMenu;
using ReMod.Core.VRChat;

namespace DeepCore.Client.UI.MM
{
    internal class MMUI
    {
        public static void WaitForMM()
        {
            new ReMMUserButton("Invite x10", "Invite x10.", delegate ()
            {
                InvSpammer.Spasm();
            }, null, MMenuPrefabs.MMUserDetailButton.transform.parent);
        }
    }
}