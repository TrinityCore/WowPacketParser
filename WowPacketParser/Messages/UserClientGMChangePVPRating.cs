using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientGMChangePVPRating
    {
        public int Season;
        public int Bracket;
        public int Rating;
    }
}
