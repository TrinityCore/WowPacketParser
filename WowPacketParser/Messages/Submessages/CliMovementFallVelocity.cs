using WowPacketParser.Misc;

namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliMovementFallVelocity
    {
        public Vector2 Direction;
        public float Speed;
    }
}
