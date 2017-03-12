using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSendRaidTargetUpdateAll
    {
        public List<RaidTargetSymbol> Targets;
        public byte PartyIndex;
    }
}
