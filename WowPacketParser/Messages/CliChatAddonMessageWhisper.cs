using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatAddonMessageWhisper
    {
        public string Prefix;
        public string Target;
        public string Text;
    }
}
