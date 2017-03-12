namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientChallengeModeMember
    {
        public ulong Guid;
        public uint VirtualRealmAddress;
        public uint NativeRealmAddress;
        public int SpecializationID;
    }
}
