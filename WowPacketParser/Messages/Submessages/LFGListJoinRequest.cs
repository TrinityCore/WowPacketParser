namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct LFGListJoinRequest
    {
        public uint ActivityID;
        public uint RequiredItemLevel;
        public string Name;
        public string Comment;
        public string VoiceChat;
    }
}
