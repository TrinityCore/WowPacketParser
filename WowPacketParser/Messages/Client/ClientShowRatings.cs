namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientShowRatings
    {
        public fixed int PersonalRating[6];
        public fixed float GdfRating[6];
        public fixed float GdfVariance[6];
    }
}
