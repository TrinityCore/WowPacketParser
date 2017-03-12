using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSplineSetSwimSpeed
    {
        public ulong MoverGUID;
        public float Speed;
    }
}
