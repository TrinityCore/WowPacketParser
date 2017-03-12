namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuctionOwnerBidNotification
    {
        public ClientAuctionOwnerNotification Info;
        public ulong Bidder;
        public ulong MinIncrement;
    }
}
