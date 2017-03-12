using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliAuctionListOwnerItems
    {
        public ulong Auctioneer;
        public uint Offset;
    }
}
