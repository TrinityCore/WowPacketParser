using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPVPSeason
    {
        public int PreviousSeason;
        public int CurrentSeason;
    }
}
