using System.Collections.Generic;
using [[[project_name]]].Helpers;
using [[[project_name]]].Managers;

namespace [[[project_name]]].Modules.ConsoleCommands {
    public class LoopLimitCommand : ConsoleCmdAbstract {
        public override string getHelp() {
            return LocaleManager.Instance.LocalizeWithPrefix("cmd_looplimit_help");
        }

        public override string[] getCommands() {
            return new[] { "[[[command_prefix_short]]]_ll", "[[[command_prefix_short]]]_looplimit", "[[[command_prefix_long]]]_looplimit" };
        }

        public override string getDescription() {
            return LocaleManager.Instance.LocalizeWithPrefix("cmd_looplimit_desc");
        }

        public override void Execute(List<string> _params, CommandSenderInfo _senderInfo) {
            if (_params.Count == 0) {
                var loopLimit = ConfigManager.Instance.Config.LoopLimit > 0
                    ? ConfigManager.Instance.Config.LoopLimit.ToString()
                    : LocaleManager.Instance.Localize("infinite");
                SdtdConsole.Instance.Output(
                    LocaleManager.Instance.LocalizeWithPrefix("cmd_looplimit_state", loopLimit));
                return;
            }

            if (!CommandHelper.ValidateCount(_params, 1)) return;
            if (!CommandHelper.ValidateType(_params[0], 1, out int newValue)) return;

            ConfigManager.Instance.Config.LoopLimit = newValue;
            ConfigManager.Instance.SaveToFile();
            [[[project_name]]]Manager.Instance.UpdateLoopState();
            SdtdConsole.Instance.Output(LocaleManager.Instance.LocalizeWithPrefix("cmd_looplimit_return", newValue));
        }
    }
}