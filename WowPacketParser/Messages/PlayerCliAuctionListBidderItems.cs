using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliAuctionListBidderItems
    {
        public uint Offset;
        public List<uint> AuctionItemIDs;
        public ulong Auctioneer;
    }
}
