using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientInstanceEncounterGainCombatResurrectionCharge
    {
        public uint CombatResChargeRecovery;
        public int InCombatResCount;
    }
}
