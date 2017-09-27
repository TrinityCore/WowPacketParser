namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct StartWarGame
    {
        public ulong OpposingPartyMember;
        public ulong QueueID;
        public bool TournamentRules;
        public uint OpposingPartyMemberVirtualRealmAddress;
        public uint OpposingPartyMemberCfgRealmID;
    }
}
