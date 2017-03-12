using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
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
