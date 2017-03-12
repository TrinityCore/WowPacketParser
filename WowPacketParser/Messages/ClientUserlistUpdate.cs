using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientUserlistUpdate
    {
        public byte ChannelFlags;
        public byte UserFlags;
        public string ChannelName;
        public ulong UpdatedUserGUID;
        public int ChannelID;
    }
}
