using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlefieldStatus_Failed
    {
        public ulong QueueID;
        public ulong ClientID;
        public int Reason;
        public CliRideTicket Ticket;
    }
}
