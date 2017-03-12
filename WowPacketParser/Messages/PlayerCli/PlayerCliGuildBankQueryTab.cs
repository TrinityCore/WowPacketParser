namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliGuildBankQueryTab
    {
        public ulong Banker;
        public bool FullUpdate;
        public byte Tab;
    }
}
