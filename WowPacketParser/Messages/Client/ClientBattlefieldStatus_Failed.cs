using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientBattlefieldStatus_Failed
    {
        public ulong QueueID;
        public ulong ClientID;
        public int Reason;
        public CliRideTicket Ticket;
    }
}
