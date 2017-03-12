using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliRideTicket
    {
        public ulong RequesterGuid;
        public uint Id;
        public uint Type;
        public UnixTime Time;
    }
}
