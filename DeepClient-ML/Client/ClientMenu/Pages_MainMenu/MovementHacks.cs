using ReMod.Core.Managers;
using ReMod.Core.UI.QuickMenu;
using VRC.SDKBase;

namespace DeepCore.Client.ClientMenu.Pages_MainMenu
{
    internal class MovementHacks
    {
        public static void MovementHacksMenu(UiManager UIManager)
        {
            ReCategoryPage reCategoryPage = UIManager.QMMenu.AddCategoryPage("Movements Functions", null);
            reCategoryPage.AddCategory("Functions");
            ReMenuCategory category = reCategoryPage.GetCategory("Functions");
            category.AddToggle("Jetpack", "I discovered the jetpack.", delegate (bool s)
            {
                Module.Movement.Jetpack.Jetpackbool = s;
            });
            category.AddButton("Flight/Noclip", "Allow you to fly like a bird.", delegate
            {
                Module.Movement.Flight.FlyToggle();
            });
            category.AddToggle("SpinBot", "Spin like a blablade.", delegate (bool s)
            {
                Module.Movement.SpinBot.SpinBotbool = s;
            }); 
            category.AddToggle("RayCast TP", "Hold ctrl and RClick.", delegate (bool s)
            {
                Module.Movement.RayCastTP.Enabled = s;
            });
            category.AddToggle("HeadFlipper", "Make your head flipping.", delegate (bool s)
            {
                Module.Movement.HeadFlipper.state(s);
            });
            category.AddToggle("PosSaver", "Allow you to save your currnt pos and reset it when disabled.", delegate (bool s)
            {
                Module.Movement.PosSaver.State(s);
            });
            category.AddToggle("Force Jump", "Allow you to jump in game worlds.", delegate (bool s)
            {
                Module.Movement.ForceJump.State(s);
            });
            category.AddButton("T-Pose", "Allow you to T-Pose.", delegate
            {
                Module.Movement.TPose.State();
            });
            reCategoryPage.AddSliderCategory("Movements Settings");
            ReMenuSliderCategory category1 = reCategoryPage.GetSliderCategory("Movements Settings");
            category1.AddSlider("Flight Speed", "Set walk speed.", delegate (float s)
            {
                Module.Movement.Flight.FlySpeed = s;
            }, 10f, 10f, 50f);
            category1.AddSlider("SpinBot Speed", "Set spinbot speed.", delegate (float s)
            {
                Module.Movement.SpinBot.rotationSpeed = s;
            }, 120f, 120f, 390f);
            category1.AddSlider("Walk Speed", "Set walk speed.", delegate (float s) 
            {
                Networking.LocalPlayer.SetWalkSpeed(s);
            },2f,2f,15f);
            category1.AddSlider("Run Speed", "Set run speed.", delegate (float s)
            {
                Networking.LocalPlayer.SetRunSpeed(s);
            },4f,2f,20f);
            category1.AddSlider("Strafe Speed", "Set strafe speed.", delegate (float s)
            {
                Networking.LocalPlayer.SetStrafeSpeed(s);
            }, 4f, 2f, 20f);
        }
    }
}
