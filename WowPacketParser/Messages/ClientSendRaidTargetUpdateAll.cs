using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSendRaidTargetUpdateAll
    {
        public List<RaidTargetSymbol> Targets;
        public byte PartyIndex;
    }
}
