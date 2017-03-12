using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatGMChat
    {
        public string Source;
        public string Arguments;
        public string Dest;
        public ulong Target;
    }
}
