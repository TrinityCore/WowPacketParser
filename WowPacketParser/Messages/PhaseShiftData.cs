using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PhaseShiftData
    {
        public uint PhaseShiftFlags;
        public List<PhaseShiftDataPhase> Phases;
        public ulong PersonalGUID;
    }
}
