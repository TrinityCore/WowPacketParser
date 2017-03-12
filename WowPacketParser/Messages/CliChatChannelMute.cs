using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatChannelMute
    {
        public string ChannelName;
        public string Name;
    }
}
