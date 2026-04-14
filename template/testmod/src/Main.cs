using HarmonyLib;

namespace testmod {
    public class Main : IModApi {
        
        private static Harmony? _harmony;

        public void InitMod(Mod modInstance) {
            _harmony ??= new Harmony("GluckHouse.testmod");
            _harmony.PatchAll();
            Log.Out("testmod initialized.");
        }
    }
}
