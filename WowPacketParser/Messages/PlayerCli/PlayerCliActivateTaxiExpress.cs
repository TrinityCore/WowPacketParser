using System.Collections.Generic;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliActivateTaxiExpress
    {
        public ulong Unit;
        public List<uint> PathNodes;
    }
}
