namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientMoveSetVehicleRecID
    {
        public ulong MoverGUID;
        public uint SequenceIndex;
        public int VehicleRecID;
    }
}
