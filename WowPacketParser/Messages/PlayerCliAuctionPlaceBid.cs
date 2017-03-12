using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliAuctionPlaceBid
    {
        public ulong Auctioneer;
        public ulong BidAmount;
        public int AuctionItemID;
    }
}
