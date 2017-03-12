using System.Collections.Generic;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct PhaseShiftData
    {
        public uint PhaseShiftFlags;
        public List<PhaseShiftDataPhase> Phases;
        public ulong PersonalGUID;
    }
}
