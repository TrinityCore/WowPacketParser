namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct LFGuildBrowseData
    {
        public string GuildName;
        public ulong GuildGUID;
        public uint GuildVirtualRealm;
        public int GuildLevel;
        public int GuildMembers;
        public int GuildAchievementPoints;
        public int PlayStyle;
        public int Availability;
        public int ClassRoles;
        public int LevelRange;
        public int EmblemStyle;
        public int EmblemColor;
        public int BorderStyle;
        public int BorderColor;
        public int Background;
        public string Comment;
        public sbyte Cached;
        public sbyte MembershipRequested;
    }
}
