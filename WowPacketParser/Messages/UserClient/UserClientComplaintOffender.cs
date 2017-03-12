namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientComplaintOffender
    {
        public ulong PlayerGuid;
        public uint RealmAddress;
        public uint TimeSinceOffence;
    }
}
