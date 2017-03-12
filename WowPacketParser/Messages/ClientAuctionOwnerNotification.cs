using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAuctionOwnerNotification
    {
        public int AuctionItemID;
        public ulong BidAmount;
        public ItemInstance Item;
    }
}
