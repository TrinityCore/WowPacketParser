using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGMPhaseAdd
    {
        public List<ushort> PhaseID;
    }
}
