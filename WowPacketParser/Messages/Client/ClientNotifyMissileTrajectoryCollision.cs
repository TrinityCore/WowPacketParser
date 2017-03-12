using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientNotifyMissileTrajectoryCollision
    {
        public ulong Caster;
        public Vector3 CollisionPos;
        public byte CastID;
    }
}
