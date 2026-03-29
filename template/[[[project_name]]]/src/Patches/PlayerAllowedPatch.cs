using HarmonyLib;
using [[[project_name]]].Managers;
using [[[project_name]]].Models;
using [[[project_name]]].Repositories;

namespace [[[project_name]]].Patches {
    [HarmonyPatch(typeof(AuthorizationManager), nameof(AuthorizationManager.playerAllowed))]
    public class PlayerAllowedPatch {
        private static void Postfix(ClientInfo _clientInfo, AuthorizationManager __instance) {
            if (!Main.IsDedicatedServer())
                return;

            if (_clientInfo.PlatformId == null)
                return;
            Log.Out(LocaleManager.Instance.LocalizeWithPrefix("log_player_connected"));
            [[[project_name]]]Manager.Instance.UpdateLoopState();

            var playerData = new PlayerRepository().GetPlayerData(_clientInfo);

            if (playerData != null)
                return;

            var plyData = new PlayerModel(_clientInfo);
            ConfigManager.Instance.Config.Players.Add(plyData);
            ConfigManager.Instance.SaveToFile();

            Log.Out(LocaleManager.Instance.LocalizeWithPrefix("log_player_new", plyData.playerName, plyData.id));
        }
    }
}