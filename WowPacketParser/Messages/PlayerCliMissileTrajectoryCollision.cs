using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliMissileTrajectoryCollision
    {
        public ulong CasterGUID;
        public Vector3 CollisionPos;
        public int SpellID;
        public byte CastID;
    }
}
