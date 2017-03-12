namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliMovementAck
    {
        public CliMovementStatus Status;
        public uint AckIndex;
    }
}
