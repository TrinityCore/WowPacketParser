using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetBattleQueueStatus
    {
        public int Status;
        public List<int> SlotResult;
        public CliRideTicket Ticket;
        public long? AverageWaitTime; // Optional
        public long? ClientWaitTime; // Optional
    }
}
