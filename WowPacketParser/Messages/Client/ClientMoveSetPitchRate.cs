namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveSetPitchRate
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
        public float Speed;
    }
}
