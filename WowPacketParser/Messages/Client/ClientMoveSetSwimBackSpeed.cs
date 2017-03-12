namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveSetSwimBackSpeed
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
        public float Speed;
    }
}
