namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct SilencePartyTalker
    {
        public bool Silence;
        public ulong Target;
        public byte PartyIndex;
    }
}
