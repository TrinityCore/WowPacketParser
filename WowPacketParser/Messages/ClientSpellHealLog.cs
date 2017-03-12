using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellHealLog
    {
        public float CritRollMade; // Optional
        public int SpellID;
        public SpellCastLogData LogData; // Optional
        public int OverHeal;
        public float CritRollNeeded; // Optional
        public bool Crit;
        public bool Multistrike;
        public int Absorbed;
        public ulong CasterGUID;
        public int Health;
        public ulong TargetGUID;
    }
}
