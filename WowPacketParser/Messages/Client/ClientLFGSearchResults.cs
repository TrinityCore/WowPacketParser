using System.Collections.Generic;
using WowPacketParser.Messages.Cli;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLFGSearchResults
    {
        public bool Incremental;
        public List<ulong> Removes;
        public CliRideTicket Ticket;
        public uint SlotID;
        public uint CountTotalParties;
        public uint CountTotalPlayers;
        public uint SlotType;
        public List<ClientLFGSearchResultParty> Parties;
        public List<ClientLFGSearchResultPlayer> Players;
    }
}
