namespace testmod
{
    public class Main : IModApi
    {
        public void InitMod(Mod modInstance)
        {
            Log.Out("testmod initialized.");
        }
    }
}
