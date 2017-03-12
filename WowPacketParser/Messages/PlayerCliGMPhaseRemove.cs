using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGMPhaseRemove
    {
        public List<ushort> PhaseID;
    }
}
