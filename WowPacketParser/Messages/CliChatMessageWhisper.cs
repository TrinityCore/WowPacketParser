using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatMessageWhisper
    {
        public string Target;
        public string Text;
        public int Language;
    }
}
