using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ChannelPlayer
    {
        public ulong Guid;
        public uint VirtualRealmAddress;
        public byte Flags;
    }
}
