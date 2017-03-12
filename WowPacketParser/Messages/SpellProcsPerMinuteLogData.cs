using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct SpellProcsPerMinuteLogData
    {
        public bool Proc;
        public int SpellID;
        public float BaseProcRate;
        public uint Now;
        public uint LastChanceTimestamp;
        public uint LastProcTimestamp;
        public uint LastChanceTime;
        public uint LastProcTime;
        public bool NormalizeAuraTime;
        public uint AuraTime;
        public float IntervalsSinceLastProc;
        public float UnluckyStreakDef;
        public float UnluckyMultiplier;
        public int ReallyUnluckyDef;
        public float ProcChance;
        public float ProcRate;
        public float Roll;
        public List<SpellProcsPerMinuteCalc> CalcHistory;
    }
}
