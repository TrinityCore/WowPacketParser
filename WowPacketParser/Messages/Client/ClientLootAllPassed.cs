using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLootAllPassed
    {
        public ulong LootObj;
        public LootItem Item;
    }
}
