namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSetPartyLeader
    {
        public ulong Target;
        public byte PartyIndex;
    }
}
