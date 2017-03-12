namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliPartyPlayerInfo
    {
        public string Name;
        public ulong Guid;
        public byte Connected;
        public byte Subgroup;
        public byte Flags;
        public byte RolesAssigned;
    }
}
