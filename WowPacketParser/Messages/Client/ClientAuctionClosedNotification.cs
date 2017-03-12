namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientAuctionClosedNotification
    {
        public ClientAuctionOwnerNotification Info;
        public float ProceedsMailDelay;
        public bool Sold;
    }
}
