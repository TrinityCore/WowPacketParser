using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientResumeCastBar
    {
        public int SpellID;
        public ulong Guid;
        public ulong Target;
        public uint TimeRemaining;
        public bool HasInterruptImmunities;
        public int SchoolImmunities;
        public int Immunities;
        public uint TotalTime;
    }
}
