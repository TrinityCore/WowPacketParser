namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliLootRoll
    {
        public ulong LootObj;
        public byte LootListID;
        public byte RollType;
    }
}
