namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientChangeSubGroup
    {
        public ulong Target;
        public byte Subgroup;
        public byte PartyIndex;
    }
}
