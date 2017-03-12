using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientNotifyMissileTrajectoryCollision
    {
        public ulong Caster;
        public Vector3 CollisionPos;
        public byte CastID;
    }
}
