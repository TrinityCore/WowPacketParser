namespace WowPacketParser.Messages.Submessages
{
    public unsafe struct CliRideTicket
    {
        public ulong RequesterGuid;
        public uint Id;
        public uint Type;
        public UnixTime Time;
    }
}
