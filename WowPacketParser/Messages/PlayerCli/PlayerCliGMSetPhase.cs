using System.Collections.Generic;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliGMSetPhase
    {
        public List<ushort> PhaseID;
    }
}
