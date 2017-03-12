namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientTextEmote
    {
        public ulong SourceGUID;
        public ulong TargetGUID;
        public int SoundIndex;
        public int EmoteID;
    }
}
