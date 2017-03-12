using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientBattlePayDistributionAssignToTarget
    {
        public ulong TargetCharacter;
        public ulong DistributionID;
        public uint ProductChoice;
        public uint ClientToken;
    }
}
