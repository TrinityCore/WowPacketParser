using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGQueueStatus
    {
        public CliRideTicket Ticket;
        public uint QueuedTime;
        public uint AvgWaitTime;
        public uint Slot;
        public uint AvgWaitTimeMe;
        public fixed byte LastNeeded[3];
        public fixed uint AvgWaitTimeByRole[3];
    }
}
