namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct DBQuery
    {
        public ulong Guid;
        public uint TableHash;
        public int RecordID;
    }
}
