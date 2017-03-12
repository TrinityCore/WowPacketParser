namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PVPBracketData
    {
        public int Rating;
        public int Rank;
        public int WeeklyPlayed;
        public int WeeklyWon;
        public int SeasonPlayed;
        public int SeasonWon;
        public int WeeklyBestRating;
        public byte Bracket;
    }
}
