using System.Collections.Generic;
using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
