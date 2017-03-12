namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLootRemoved
    {
        public ulong LootObj;
        public ulong Owner;
        public byte LootListID;
    }
}
