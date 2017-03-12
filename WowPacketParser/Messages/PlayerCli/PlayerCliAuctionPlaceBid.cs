namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliAuctionPlaceBid
    {
        public ulong Auctioneer;
        public ulong BidAmount;
        public int AuctionItemID;
    }
}
