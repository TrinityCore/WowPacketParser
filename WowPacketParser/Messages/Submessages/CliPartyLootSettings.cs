namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliPartyLootSettings
    {
        public ulong LootMaster;
        public byte LootMethod;
        public byte LootThreshold;
    }
}
