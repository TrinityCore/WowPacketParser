namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliTestDropRate
    {
        public uint TreasureID;
        public uint? ItemQuality; // Optional
        public int? ClassID; // Optional
        public uint? ItemID; // Optional
        public int? SubClassID; // Optional
        public int LootLevel;
    }
}
