namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientDBQuery
    {
        public ulong Guid;
        public uint TableHash;
        public int RecordID;
    }
}
