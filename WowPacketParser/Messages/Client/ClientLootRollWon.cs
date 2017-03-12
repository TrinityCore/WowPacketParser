using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLootRollWon
    {
        public ulong Winner;
        public ulong LootObj;
        public byte RollType;
        public int Roll;
        public LootItem Item;
    }
}
