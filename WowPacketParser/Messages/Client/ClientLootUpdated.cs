using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLootUpdated
    {
        public LootItem Item;
        public ulong LootObj;
    }
}
