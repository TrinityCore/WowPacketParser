namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLootList
    {
        public ulong? RoundRobinWinner; // Optional
        public ulong? Master; // Optional
        public ulong Owner;
    }
}
