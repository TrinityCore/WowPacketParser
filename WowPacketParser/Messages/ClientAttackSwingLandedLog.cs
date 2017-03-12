using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAttackSwingLandedLog
    {
        public SpellCastLogData LogData;
        public Data AttackRoundInfo;
    }
}
