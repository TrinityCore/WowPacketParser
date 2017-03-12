namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLFGProposalUpdatePlayer
    {
        public uint Roles;
        public bool Me;
        public bool SameParty;
        public bool MyParty;
        public bool Responded;
        public bool Accepted;
    }
}
