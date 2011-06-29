using WowPacketParser.Enums;

namespace WowPacketParser.Misc
{
    public sealed class MovementInfo
    {
        public MovementFlag Flags;

        public Vector3 Position;

        public float Orientation;

        public float WalkSpeed;

        public float RunSpeed;
    }
}
