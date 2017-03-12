namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientRoleChangedInform
    {
        public ulong ChangedUnit;
        public ulong From;
        public uint NewRole;
        public uint OldRole;
        public byte PartyIndex;
    }
}
