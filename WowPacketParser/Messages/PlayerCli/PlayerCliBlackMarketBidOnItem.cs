using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliBlackMarketBidOnItem
    {
        public ulong NpcGUID;
        public ItemInstance Item;
        public int MarketID;
        public ulong BidAmount;
    }
}
