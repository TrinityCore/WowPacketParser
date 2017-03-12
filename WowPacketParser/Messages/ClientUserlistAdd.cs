using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientUserlistAdd
    {
        public byte ChannelFlags;
        public byte UserFlags;
        public string ChannelName;
        public ulong AddedUserGUID;
        public int ChannelID;
    }
}
