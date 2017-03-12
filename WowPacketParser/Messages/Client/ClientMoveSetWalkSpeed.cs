namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveSetWalkSpeed
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
        public float Speed;
    }
}
