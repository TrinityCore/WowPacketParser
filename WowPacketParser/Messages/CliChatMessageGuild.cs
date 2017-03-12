using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatMessageGuild
    {
        public int Language;
        public string Text;
    }
}
