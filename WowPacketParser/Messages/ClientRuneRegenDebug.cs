using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRuneRegenDebug
    {
        public uint Time;
        public uint Interval;
        public fixed int RuneStart[6];
        public fixed int RuneEnd[6];
        public fixed float RegenRate[4];
    }
}
