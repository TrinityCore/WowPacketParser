using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSimulateTickets
    {
        public uint Seed;
        public uint Count;
        public uint RideType;
    }
}
