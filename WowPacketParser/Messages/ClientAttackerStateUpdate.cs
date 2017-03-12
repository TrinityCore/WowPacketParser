using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAttackerStateUpdate
    {
        public SpellCastLogData? LogData; // Optional
        public Data AttackRoundInfo;
    }
}
