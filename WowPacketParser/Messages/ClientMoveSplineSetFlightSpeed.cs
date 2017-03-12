using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSplineSetFlightSpeed
    {
        public ulong MoverGUID;
        public float Speed;
    }
}
