namespace DeepCore.Client.Module.Visual
{
    internal class UpdateModule
    {
        public static void OnUpdate()
        {
            OptifineZoom.Update();
            LineESP.UpdateLines();
            BoxESP.UpdateBox();
        }
    }
}
