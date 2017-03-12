using System.Collections.Generic;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliGMPhaseRemove
    {
        public List<ushort> PhaseID;
    }
}
