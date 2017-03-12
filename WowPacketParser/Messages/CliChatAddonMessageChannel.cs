using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatAddonMessageChannel
    {
        public string Text;
        public string Target;
        public string Prefix;
    }
}
