using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBlackMarketWon
    {
        public int MarketID;
        public int RandomPropertiesID;
        public ItemInstance Item;
    }
}
