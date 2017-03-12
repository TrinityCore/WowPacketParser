using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellDamageShield
    {
        public int SpellID;
        public ulong Defender;
        public SpellCastLogData LogData; // Optional
        public int OverKill;
        public int TotalDamage;
        public ulong Attacker;
        public int LogAbsorbed;
        public int SchoolMask;
    }
}
