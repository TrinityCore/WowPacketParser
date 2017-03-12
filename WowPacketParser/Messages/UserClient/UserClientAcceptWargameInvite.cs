namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientAcceptWargameInvite
    {
        public ulong OpposingPartyMember;
        public ulong QueueID;
        public bool Accept;
    }
}
