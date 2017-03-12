using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientGMPhaseDump
    {
        public PhaseShiftData PhaseShift;
        public ulong Target;
    }
}
