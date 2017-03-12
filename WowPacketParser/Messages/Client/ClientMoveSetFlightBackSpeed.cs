namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveSetFlightBackSpeed
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
        public float Speed;
    }
}
