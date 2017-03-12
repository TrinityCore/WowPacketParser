namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientPartyUninvite
    {
        public string Reason;
        public byte PartyIndex;
        public ulong TargetGuid;
    }
}
