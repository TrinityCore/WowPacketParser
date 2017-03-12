using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliChatChannelPassword
    {
        public string Name;
        public string ChannelName;
    }
}
