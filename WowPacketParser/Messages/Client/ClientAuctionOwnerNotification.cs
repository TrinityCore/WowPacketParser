using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuctionOwnerNotification
    {
        public int AuctionItemID;
        public ulong BidAmount;
        public ItemInstance Item;
    }
}
