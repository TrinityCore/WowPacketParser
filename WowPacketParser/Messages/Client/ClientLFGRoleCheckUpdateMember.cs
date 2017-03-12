namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLFGRoleCheckUpdateMember
    {
        public ulong Guid;
        public bool RoleCheckComplete;
        public uint RolesDesired;
        public byte Level;
    }
}
