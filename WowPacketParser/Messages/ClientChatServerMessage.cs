using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientChatServerMessage
    {
        public string StringParam;
        public int MessageID;
    }
}
