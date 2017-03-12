using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSplineSetRunSpeed
    {
        public ulong MoverGUID;
        public float Speed;
    }
}
