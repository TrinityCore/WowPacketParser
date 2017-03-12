using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSplineSetRunBackSpeed
    {
        public ulong MoverGUID;
        public float Speed;
    }
}
