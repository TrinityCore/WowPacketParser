using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientInstanceEncounterStart
    {
        public uint NextCombatResChargeTime;
        public uint CombatResChargeRecovery;
        public int MaxInCombatResCount;
        public int InCombatResCount;
    }
}
