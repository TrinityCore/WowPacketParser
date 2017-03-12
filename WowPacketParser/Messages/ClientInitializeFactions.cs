using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientInitializeFactions
    {
        public fixed int FactionStandings[256];
        public fixed bool FactionHasBonus[256];
        public fixed byte FactionFlags[256];
    }
}
