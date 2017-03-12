using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGMPhaseDump
    {
        public PhaseShiftData PhaseShift;
        public ulong Target;
    }
}
