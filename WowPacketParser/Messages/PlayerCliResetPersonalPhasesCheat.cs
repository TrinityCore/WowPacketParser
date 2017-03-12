using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliResetPersonalPhasesCheat
    {
        public bool AllPhases;
        public List<ushort> PhaseIDs;
    }
}
