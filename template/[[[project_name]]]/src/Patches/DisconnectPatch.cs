using HarmonyLib;
using [[[project_name]]].Managers;

namespace [[[project_name]]].Patches {
    [HarmonyPatch(typeof(ConnectionManager), nameof(ConnectionManager.DisconnectClient))]
    public class DisconnectPatch {
        private static void Postfix(ConnectionManager __instance) {
            if (!Main.IsDedicatedServer())
                return;
            Log.Out(LocaleManager.Instance.Localize("log_player_disconnected"));
            [[[project_name]]]Manager.Instance.UpdateLoopState();
        }
    }
}