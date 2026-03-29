using System.Collections.Generic;
using [[[project_name]]].Helpers;
using [[[project_name]]].Managers;

namespace [[[project_name]]].Modules.ConsoleCommands {
    public class EnableCommand : ConsoleCmdAbstract {
        public override string getHelp() {
            return LocaleManager.Instance.Localize("cmd_enable_help");
        }

        public override string[] getCommands() {
            return new[] { "[[[command_prefix_short]]]_enable", "[[[command_prefix_long]]]_enable", "[[[command_prefix_long]]]" };
        }

        public override string getDescription() {
            return LocaleManager.Instance.Localize("cmd_enable_desc");
        }

        public override void Execute(List<string> _params, CommandSenderInfo _senderInfo) {
            if (_params.Count == 0) {
                SdtdConsole.Instance.Output(LocaleManager.Instance.LocalizeWithPrefix("cmd_enable_state",
                    ConfigManager.Instance.Config.Enabled));
                return;
            }

            if (!CommandHelper.ValidateCount(_params, 1)) return;
            if (!CommandHelper.ValidateType(_params[0], 1, out int newValue)) return;

            ConfigManager.Instance.Config.Enabled = newValue >= 1;
            ConfigManager.Instance.SaveToFile();
            [[[project_name]]]Manager.Instance.UpdateLoopState();
            var newState = ConfigManager.Instance.Config.Enabled
                ? LocaleManager.Instance.Localize("enabled")
                : LocaleManager.Instance.Localize("disabled");
            SdtdConsole.Instance.Output(LocaleManager.Instance.LocalizeWithPrefix("cmd_enable_return", newState));
        }
    }
}