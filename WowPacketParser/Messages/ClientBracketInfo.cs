using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBracketInfo
    {
        public int PersonalRating;
        public int Ranking;
        public int SeasonPlayed;
        public int SeasonWon;
        public int WeeklyPlayed;
        public int WeeklyWon;
        public int BestWeeklyRating;
        public int BestSeasonRating;
    }
}
