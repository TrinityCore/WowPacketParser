namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveSetRunBackSpeed
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
        public float Speed;
    }
}
