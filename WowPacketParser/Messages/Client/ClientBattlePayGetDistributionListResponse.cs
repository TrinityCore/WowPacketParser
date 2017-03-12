using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlePayGetDistributionListResponse
    {
        public uint Result;
        public List<BattlePayDistributionObject> DistributionObjects;
    }
}
