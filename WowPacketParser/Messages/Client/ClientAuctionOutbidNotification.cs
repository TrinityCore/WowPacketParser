namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuctionOutbidNotification
    {
        public ClientAuctionBidderNotification Info;
        public ulong BidAmount;
        public ulong MinIncrement;
    }
}
