namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliPartyLootSettings
    {
        public ulong LootMaster;
        public byte LootMethod;
        public byte LootThreshold;
    }
}
