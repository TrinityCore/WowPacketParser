namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct HotfixNotify
    {
        public uint TableHash;
        public int RecordID;
        public uint Timestamp;
    }
}
