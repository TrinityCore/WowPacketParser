namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct LFGuildApplicationData
    {
        public ulong GuildGUID;
        public uint GuildVirtualRealm;
        public string GuildName;
        public int ClassRoles;
        public int PlayStyle;
        public int Availability;
        public uint SecondsSinceCreated;
        public uint SecondsUntilExpiration;
        public string Comment;
    }
}
