namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliMailTakeMoney
    {
        public ulong Mailbox;
        public ulong Money;
        public int MailID;
    }
}
