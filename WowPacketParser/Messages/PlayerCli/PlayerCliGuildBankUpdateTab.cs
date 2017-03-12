namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliGuildBankUpdateTab
    {
        public string Icon;
        public string Name;
        public byte BankTab;
        public ulong Banker;
    }
}
