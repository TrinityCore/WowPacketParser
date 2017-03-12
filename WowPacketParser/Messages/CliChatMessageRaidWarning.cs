using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatMessageRaidWarning
    {
        public int Language;
        public string Text;
    }
}
