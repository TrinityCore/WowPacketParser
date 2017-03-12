namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct GuildMemberSetNoteData
    {
        public ulong NoteeGUID;
        public string Note;
        public bool IsPublic;
    }
}
