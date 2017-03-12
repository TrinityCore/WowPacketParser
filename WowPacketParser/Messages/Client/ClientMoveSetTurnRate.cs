namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveSetTurnRate
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
        public float Speed;
    }
}
