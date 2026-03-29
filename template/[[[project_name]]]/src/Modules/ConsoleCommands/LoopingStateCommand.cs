using System.Collections.Generic;
using [[[project_name]]].Managers;

namespace [[[project_name]]].Modules.ConsoleCommands {
    public class LoopingStateCommand : ConsoleCmdAbstract {
        public override string getHelp() {
            return LocaleManager.Instance.Localize("cmd_loopstate_help");
        }

        public override string[] getCommands() {
            return new[] { "[[[command_prefix_short]]]_state", "[[[command_prefix_long]]]_state" };
        }

        public override string getDescription() {
            return LocaleManager.Instance.LocalizeWithPrefix("cmd_loopstate_desc");
        }

        public override void Execute(List<string> _params, CommandSenderInfo _senderInfo) {
            var isOrNot = [[[project_name]]]Manager.Instance.IsTimeFlowing ? "is_not" : "is";
            SdtdConsole.Instance.Output(LocaleManager.Instance.LocalizeWithPrefix("cmd_loopstate_return",
                LocaleManager.Instance.Localize(isOrNot)));
        }
    }
}