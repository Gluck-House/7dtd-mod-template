using System.Collections.Generic;
using [[[project_name]]].Helpers;
using [[[project_name]]].Managers;

namespace [[[project_name]]].Modules.ConsoleCommands {
    public class MinPlayersCommand : ConsoleCmdAbstract {
        public override string GetHelp() {
            return LocaleManager.Instance.Localize("cmd_minplayers_help");
        }

        public override string[] getCommands() {
            return new[] { "[[[command_prefix_short]]]_min", "[[[command_prefix_short]]]_minplayers", "[[[command_prefix_long]]]_minplayers" };
        }

        public override string getDescription() {
            return LocaleManager.Instance.LocalizeWithPrefix("cmd_minplayers_desc");
        }

        public override void Execute(List<string> _params, CommandSenderInfo _senderInfo) {
            if (_params.Count == 0) {
                SdtdConsole.Instance.Output(LocaleManager.Instance.LocalizeWithPrefix("cmd_minplayers_state",
                    ConfigManager.Instance.Config.MinPlayers));
                return;
            }

            if (!CommandHelper.ValidateCount(_params, 1)) return;
            if (!CommandHelper.ValidateType(_params[0], 1, out int newValue)) return;

            ConfigManager.Instance.Config.MinPlayers = newValue;
            ConfigManager.Instance.SaveToFile();
            [[[project_name]]]Manager.Instance.UpdateLoopState();
            SdtdConsole.Instance.Output(LocaleManager.Instance.LocalizeWithPrefix("cmd_minplayers_return", newValue));
        }
    }
}