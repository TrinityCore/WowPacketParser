using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliPushItemsFromTreasure
    {
        public uint TreasureID;
        public ItemContext LootItemContext;
    }
}
