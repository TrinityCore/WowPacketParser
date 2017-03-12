using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientChannelList
    {
        public List<ChannelPlayer> Members;
        public string Channel;
        public byte ChannelFlags;
        public bool Display;
    }
}
