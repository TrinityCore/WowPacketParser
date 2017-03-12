namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSetAssistantLeader
    {
        public bool Set;
        public byte PartyIndex;
        public ulong Target;
    }
}
