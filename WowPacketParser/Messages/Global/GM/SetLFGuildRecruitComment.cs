namespace WowPacketParser.Messages.Global.GM
{
    public unsafe struct SetLFGuildRecruitComment
    {
        public ulong PlayerGUID;
        public string Comment;
        public ulong GuildGUID;
    }
}
