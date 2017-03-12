namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientPartyInviteResponse
    {
        public byte PartyIndex;
        public bool Accept;
        public uint? RolesDesired; // Optional
    }
}
