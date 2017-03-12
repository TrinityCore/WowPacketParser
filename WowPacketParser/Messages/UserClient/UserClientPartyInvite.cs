namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientPartyInvite
    {
        public ulong TargetGuid;
        public uint ProposedRoles;
        public string TargetName;
        public byte PartyIndex;
        public string TargetRealm;
        public uint TargetCfgRealmID;
    }
}
