namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSetRole
    {
        public ulong ChangedUnit;
        public uint Role;
        public byte PartyIndex;
    }
}
