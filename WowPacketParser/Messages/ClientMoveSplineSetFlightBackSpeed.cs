using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSplineSetFlightBackSpeed
    {
        public ulong MoverGUID;
        public float Speed;
    }
}
