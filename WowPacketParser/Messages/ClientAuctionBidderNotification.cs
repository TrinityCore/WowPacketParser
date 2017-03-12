using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuctionBidderNotification
    {
        public int AuctionItemID;
        public ulong Bidder;
        public ItemInstance Item;
    }
}
