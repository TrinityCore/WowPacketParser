namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMailCommandResult
    {
        public int Command;
        public int MailID;
        public int QtyInInventory;
        public int BagResult;
        public int AttachID;
        public int ErrorCode;
    }
}
