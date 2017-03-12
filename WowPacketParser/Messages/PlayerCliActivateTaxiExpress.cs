using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliActivateTaxiExpress
    {
        public ulong Unit;
        public List<uint> PathNodes;
    }
}
