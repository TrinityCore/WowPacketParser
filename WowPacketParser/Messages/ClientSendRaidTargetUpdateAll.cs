using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSendRaidTargetUpdateAll
    {
        public List<RaidTargetSymbol> Targets;
        public byte PartyIndex;
    }
}
