namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveSetFlightSpeed
    {
        public ulong MoverGUID;
        public float Speed;
        public uint SequenceIndex;
    }
}
