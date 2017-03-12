using WowPacketParser.Messages.Submessages;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientOnMonsterMove
    {
        public MovementMonsterSpline SplineData;
        public ulong MoverGUID;
        public Vector3 Position;
    }
}
