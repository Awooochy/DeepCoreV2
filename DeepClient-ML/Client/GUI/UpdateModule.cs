namespace DeepCore.Client.GUI
{
    internal class UpdateModule
    {
        private static TestModMenu modMenu;
        public static void StartAll()
        {
            modMenu = new TestModMenu();
            modMenu.Start();    
        }
        public static void UpdateGUI()
        {
            LineESP.Render();
        }
        public static void OnUpdate()
        {
        }
    }
}
