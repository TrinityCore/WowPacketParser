using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBlackMarketOutbid
    {
        public ItemInstance Item;
        public int MarketID;
        public int RandomPropertiesID;
    }
}
