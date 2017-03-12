using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientBattlePayDistributionSelectChoice
    {
        public ulong DistributionID;
        public ulong TargetCharacter;
        public uint ProductChoice;
        public uint ClientToken;
    }
}
