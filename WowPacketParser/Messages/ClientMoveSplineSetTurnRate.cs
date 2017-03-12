using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSplineSetTurnRate
    {
        public ulong MoverGUID;
        public float Speed;
    }
}
