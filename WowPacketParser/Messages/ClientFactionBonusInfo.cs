using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientFactionBonusInfo
    {
        public fixed bool FactionHasBonus[256];
    }
}
