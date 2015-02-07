namespace WowPacketParser.Enums
{
    public enum AuctionHouseAction
    {
        Sell   = 0, // ERR_AUCTION_STARTED
        Cancel = 1, // ERR_AUCTION_REMOVED
        Bid    = 2  // ERR_AUCTION_BID_PLACED
    }
}
