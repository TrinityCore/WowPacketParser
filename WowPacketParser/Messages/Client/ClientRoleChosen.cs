namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientRoleChosen
    {
        public bool Accepted;
        public uint RoleMask;
        public ulong Player;
    }
}
