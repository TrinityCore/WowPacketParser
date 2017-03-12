using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatChannelBan
    {
        public string Name;
        public string ChannelName;
    }
}
