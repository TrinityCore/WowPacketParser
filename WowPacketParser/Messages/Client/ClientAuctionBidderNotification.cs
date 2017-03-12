using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuctionBidderNotification
    {
        public int AuctionItemID;
        public ulong Bidder;
        public ItemInstance Item;
    }
}
