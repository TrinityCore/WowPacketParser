namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliCharacterShipment
    {
        public int ShipmentRecID;
        public ulong ShipmentID;
        public UnixTime CreationTime;
        public UnixTime ShipmentDuration;
    }
}
