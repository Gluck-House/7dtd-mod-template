using System.Collections.Generic;
using [[[project_name]]].Helpers;
using [[[project_name]]].Managers;
using [[[project_name]]].Repositories;

namespace [[[project_name]]].Modules.ConsoleCommands {
    public class AuthorizeClientCommand : ConsoleCmdAbstract {
        public override string GetHelp() {
            return LocaleManager.Instance.Localize("cmd_authorize_help");
        }

        public override string[] getCommands() {
            return new[] { "[[[command_prefix_short]]]_auth", "[[[command_prefix_short]]]_authorize", "[[[command_prefix_long]]]_auth", "[[[command_prefix_long]]]_authorize" };
        }

        public override string getDescription() {
            return LocaleManager.Instance.LocalizeWithPrefix("cmd_authorize_desc");
        }

        public override void Execute(List<string> _params, CommandSenderInfo _senderInfo) {
            if (!CommandHelper.ValidateCount(_params, 2)) return;
            if (!CommandHelper.ValidateType(_params[1], 2, out int newValue)) return;

            var playerDataRepository = new PlayerRepository();
            var playerData = playerDataRepository.GetPlayerDataNameOrId(_params[0]);
            if (playerData == null) {
                SdtdConsole.Instance.Output(
                    LocaleManager.Instance.LocalizeWithPrefix("cmd_player_not_found", _params[0]));
                return;
            }

            playerData.skip[[[project_name]]] = newValue >= 1;
            ConfigManager.Instance.SaveToFile();
            [[[project_name]]]Manager.Instance.UpdateLoopState();
            var newState = playerData.skip[[[project_name]]]
                ? LocaleManager.Instance.Localize("authorized")
                : LocaleManager.Instance.Localize("unauthorized");
            SdtdConsole.Instance.Output(
                LocaleManager.Instance.LocalizeWithPrefix("cmd_authorized_return", newState, playerData.playerName));
        }
    }
}