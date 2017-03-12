using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientUserlistRemove
    {
        public byte ChannelFlags;
        public string ChannelName;
        public int ChannelID;
        public ulong RemovedUserGUID;
    }
}
