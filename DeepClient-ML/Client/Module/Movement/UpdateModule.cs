namespace DeepCore.Client.Module.Movement
{
    internal class UpdateModule
    {
        public static void Update()
        {
            Jetpack.Update();
            SpinBot.Update();
            Flight.FlyUpdate();
            SeatOnHead.Update();
            RayCastTP.Update();
        }
    }
}
