using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellNonMeleeDamageLogDebugInfo
    {
        public float CritRoll;
        public float CritNeeded;
        public float HitRoll;
        public float HitNeeded;
        public float MissChance;
        public float DodgeChance;
        public float ParryChance;
        public float BlockChance;
        public float GlanceChance;
        public float CrushChance;
    }
}
