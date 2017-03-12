using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBlackMarketBidOnItemResult
    {
        public int MarketID;
        public ItemInstance Item;
        public int Result;
    }
}
