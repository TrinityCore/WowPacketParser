using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlePayGetDistributionListResponse
    {
        public uint Result;
        public List<BattlePayDistributionObject> DistributionObjects;
    }
}
