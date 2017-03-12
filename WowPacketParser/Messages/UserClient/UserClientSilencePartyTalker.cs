namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientSilencePartyTalker
    {
        public bool Silence;
        public ulong Target;
        public byte PartyIndex;
    }
}
