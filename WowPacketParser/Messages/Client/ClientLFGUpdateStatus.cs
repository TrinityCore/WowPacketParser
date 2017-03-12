using System.Collections.Generic;
using WowPacketParser.Messages.Cli;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientLFGUpdateStatus
    {
        public uint RequestedRoles;
        public bool NotifyUI;
        public List<uint> Slots;
        public bool LfgJoined;
        public byte Reason;
        public List<ulong> SuspendedPlayers;
        public byte SubType;
        public bool Queued;
        public string Comment;
        public CliRideTicket Ticket;
        public bool Joined;
        public bool IsParty;
        public fixed byte Needs[3];
    }
}
