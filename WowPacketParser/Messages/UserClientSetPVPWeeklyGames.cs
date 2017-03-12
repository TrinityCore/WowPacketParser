using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSetPVPWeeklyGames
    {
        public uint NumPlayed;
        public int Season;
        public uint NumWon;
        public byte Bracket;
    }
}
