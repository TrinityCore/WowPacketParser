using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientLFGProposalUpdatePlayer
    {
        public uint Roles;
        public bool Me;
        public bool SameParty;
        public bool MyParty;
        public bool Responded;
        public bool Accepted;
    }
}
