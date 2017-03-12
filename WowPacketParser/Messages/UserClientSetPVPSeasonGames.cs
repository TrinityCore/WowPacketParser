using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSetPVPSeasonGames
    {
        public uint NumWon;
        public int Season;
        public uint NumPlayed;
        public byte Bracket;
    }
}
