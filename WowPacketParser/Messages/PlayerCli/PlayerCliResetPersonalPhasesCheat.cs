using System.Collections.Generic;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliResetPersonalPhasesCheat
    {
        public bool AllPhases;
        public List<ushort> PhaseIDs;
    }
}
