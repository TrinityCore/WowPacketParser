namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientLFGuildAddRecruit
    {
        public ulong GuildGUID;
        public int Availability;
        public int ClassRoles;
        public int PlayStyle;
        public string Comment;
    }
}
