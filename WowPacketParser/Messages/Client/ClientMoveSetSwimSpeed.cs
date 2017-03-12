namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveSetSwimSpeed
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
        public float Speed;
    }
}
