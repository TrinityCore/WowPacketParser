namespace WowPacketParser.Messages.Global
{
    public unsafe struct GlobalGMSetLFGuildRecruitComment
    {
        public ulong PlayerGUID;
        public string Comment;
        public ulong GuildGUID;
    }
}
