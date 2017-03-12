using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientMoveSplineSetPitchRate
    {
        public ulong MoverGUID;
        public float Speed;
    }
}
