namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliMovementAck
    {
        public CliMovementStatus Status;
        public uint AckIndex;
    }
}
