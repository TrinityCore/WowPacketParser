using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAttackSwingLandedLog
    {
        public SpellCastLogData LogData;
        public Data AttackRoundInfo;
    }
}
