namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSocketGems
    {
        public fixed ulong GemItem[3];
        public ulong ItemGuid;
    }
}
