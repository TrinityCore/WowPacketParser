using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientFlightSplineSync
    {
        public ulong Guid;
        public float SplineDist;
    }
}
