using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientChannelList
    {
        public List<ChannelPlayer> Members;
        public string Channel;
        public byte ChannelFlags;
        public bool Display;
    }
}
