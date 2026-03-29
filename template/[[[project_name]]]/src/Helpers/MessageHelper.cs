using System.Collections.Generic;

namespace [[[project_name]]].Helpers
{
    public static class MessageHelper
    {
        public static void SendGlobalChat(string message)
        {
            GameManager.Instance.ChatMessageServer(null, EChatType.Global, -1, message, null, EMessageSender.None);
        }

        public static void SendPrivateChat(string message, ClientInfo recipient)
        {
            GameManager.Instance.ChatMessageServer(null, EChatType.Global, -1, message, new List<int>{ recipient.entityId }, EMessageSender.None);
        }
    }
}
