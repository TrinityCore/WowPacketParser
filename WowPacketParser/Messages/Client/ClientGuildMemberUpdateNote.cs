namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientGuildMemberUpdateNote
    {
        public string Note;
        public ulong Member;
        public bool IsPublic;
    }
}
