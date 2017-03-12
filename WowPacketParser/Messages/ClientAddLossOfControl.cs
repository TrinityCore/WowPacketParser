using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientAddLossOfControl
    {
        public uint DurationRemaining;
        public ulong Caster;
        public uint Duration;
        public uint LockoutSchoolMask;
        public int SpellID;
        public int Type;
        public int Mechanic;
    }
}
