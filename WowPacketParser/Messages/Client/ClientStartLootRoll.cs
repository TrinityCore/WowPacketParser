using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientStartLootRoll
    {
        public uint RollTime;
        public byte Method;
        public LootItem Item;
        public int MapID;
        public ulong LootObj;
        public byte ValidRolls;
    }
}
