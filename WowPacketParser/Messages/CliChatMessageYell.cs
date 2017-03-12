using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatMessageYell
    {
        public string Text;
        public int Language;
    }
}
