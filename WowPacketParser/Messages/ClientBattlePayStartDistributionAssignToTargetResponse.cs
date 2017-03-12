using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlePayStartDistributionAssignToTargetResponse
    {
        public ulong DistributionID;
        public uint ClientToken;
        public uint Result;
    }
}
