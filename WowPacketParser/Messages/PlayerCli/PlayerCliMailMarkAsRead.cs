namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliMailMarkAsRead
    {
        public bool BiReceipt;
        public int MailID;
        public ulong Mailbox;
    }
}
