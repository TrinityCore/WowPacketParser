namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliInspectGuildData
    {
        public ulong GuildGUID;
        public long GuildXP;
        public int GuildLevel;
        public int NumGuildMembers;
    }
}
