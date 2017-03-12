namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveRemoveMovementForce
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
        public uint MovementForceID;
    }
}
