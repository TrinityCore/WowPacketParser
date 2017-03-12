namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliInspectGuildData
    {
        public ulong GuildGUID;
        public long GuildXP;
        public int GuildLevel;
        public int NumGuildMembers;
    }
}
