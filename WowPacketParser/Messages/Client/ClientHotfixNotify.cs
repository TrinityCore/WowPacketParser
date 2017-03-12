namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientHotfixNotify
    {
        public uint TableHash;
        public uint Timestamp;
        public int RecordID;
    }
}
