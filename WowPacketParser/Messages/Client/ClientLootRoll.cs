using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLootRoll
    {
        public ulong Player;
        public int Roll;
        public LootItem Item;
        public byte RollType;
        public ulong LootObj;
        public bool Autopassed;
    }
}
