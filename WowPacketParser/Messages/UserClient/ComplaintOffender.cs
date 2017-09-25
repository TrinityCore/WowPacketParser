namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct ComplaintOffender
    {
        public ulong PlayerGuid;
        public uint RealmAddress;
        public uint TimeSinceOffence;
    }
}
