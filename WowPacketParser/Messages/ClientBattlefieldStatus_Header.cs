using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientBattlefieldStatus_Header
    {
        public CliRideTicket Ticket;
        public ulong QueueID;
        public byte RangeMin;
        public byte RangeMax;
        public byte TeamSize;
        public uint InstanceID;
        public bool RegisteredMatch;
        public bool TournamentRules;
    }
}
