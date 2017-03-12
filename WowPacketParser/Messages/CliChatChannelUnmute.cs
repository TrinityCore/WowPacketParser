using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatChannelUnmute
    {
        public string ChannelName;
        public string Name;
    }
}
