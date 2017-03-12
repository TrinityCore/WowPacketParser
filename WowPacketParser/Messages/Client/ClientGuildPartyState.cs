namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildPartyState
    {
        public float GuildXPEarnedMult;
        public int NumMembers;
        public bool InGuildParty;
        public int NumRequired;
    }
}
