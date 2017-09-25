namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct AcceptWargameInvite
    {
        public ulong OpposingPartyMember;
        public ulong QueueID;
        public bool Accept;
    }
}
