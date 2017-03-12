using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatMessageChannel
    {
        public int Language;
        public string Text;
        public string Target;
    }
}
