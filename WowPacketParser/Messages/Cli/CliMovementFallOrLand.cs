namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliMovementFallOrLand
    {
        public uint Time;
        public float JumpVelocity;
        public CliMovementFallVelocity? Velocity; // Optional
    }
}
