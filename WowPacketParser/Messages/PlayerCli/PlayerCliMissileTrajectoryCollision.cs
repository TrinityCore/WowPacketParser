using WowPacketParser.Misc;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliMissileTrajectoryCollision
    {
        public ulong CasterGUID;
        public Vector3 CollisionPos;
        public int SpellID;
        public byte CastID;
    }
}
