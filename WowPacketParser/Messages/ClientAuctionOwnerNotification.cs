using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
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
