namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSetPartyAssignment
    {
        public byte PartyIndex;
        public ulong Target;
        public byte Assignment;
        public bool Set;
    }
}
