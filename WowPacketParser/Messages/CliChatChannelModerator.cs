using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatChannelModerator
    {
        public string ChannelName;
        public string Name;
    }
}
