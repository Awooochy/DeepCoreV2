using DeepCore.Client.Misc;
using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
    internal class MediaControl
    {
        public static void MediaControlMenu(UiManager UIManager)
        {
            ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("Media Controls", null);
            reCategoryPage.AddCategory("(my computer)");
            ReMenuCategory category = reCategoryPage.GetCategory("(my computer)");
            category.AddButton("Play/Pause", "Play/Pause current playing media.", delegate
            {
                MediaKeys.PlayPause();
            });
            category.AddButton("Prev Song", "Return to the previous song.", delegate
            {
                MediaKeys.PrevTrack();
            });
            category.AddButton("Next Song", "Go to the next song.", delegate
            {
                MediaKeys.NextTrack();
            });
            category.AddButton("Stop", "Stop current playing media.", delegate
            {
                MediaKeys.Stop();
            });
            category.AddButton("Volume Up", "Let me goo like ssssukkkaaa blyat.", delegate
            {
                MediaKeys.VolumeUp();
            });
            category.AddButton("Volume Down", "Ewwwwww.", delegate
            {
                MediaKeys.VolumeDown();
            });
            category.AddButton("Mute", "Silence.", delegate
            {
                MediaKeys.VolumeMute();
            });
        }
    }
}
