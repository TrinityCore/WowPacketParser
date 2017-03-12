namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct GdfSimPlayer
    {
        public ulong Guid;
        public float GdfRating;
        public float GdfVariance;
        public int PersonalRating;
        public bool Team;
    }
}
