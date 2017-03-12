namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliMovementFallOrLand
    {
        public uint Time;
        public float JumpVelocity;
        public CliMovementFallVelocity? Velocity; // Optional
    }
}
