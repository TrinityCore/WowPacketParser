namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveSetRunSpeed
    {
        public ulong MoverGUID;
        public float Speed;
        public uint SequenceIndex;
    }
}
