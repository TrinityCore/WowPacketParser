using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientOnMonsterMove
    {
        public MovementMonsterSpline SplineData;
        public ulong MoverGUID;
        public Vector3 Position;
    }
}
