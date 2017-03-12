using System.Collections.Generic;
using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLFGJoinResult
    {
        public byte Result;
        public List<ClientLFGBlackList> BlackList;
        public byte ResultDetail;
        public CliRideTicket Ticket;
    }
}
