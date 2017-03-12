using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientChannelNotifyJoined
    {
        public string ChannelWelcomeMsg;
        public int ChatChannelID;
        public int InstanceID;
        public byte ChannelFlags;
        public string Channel;
    }
}
