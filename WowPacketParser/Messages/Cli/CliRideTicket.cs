using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Cli
{
    public unsafe struct CliRideTicket
    {
        public ulong RequesterGuid;
        public uint Id;
        public uint Type;
        public UnixTime Time;
    }
}
