namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildSendGuildXP
    {
        public long GuildXPToLevel;
        public long MemberTotalXP;
        public long MemberWeeklyXP;
        public long GuildTotalXP;
    }
}
