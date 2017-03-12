using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientStartWarGame
    {
        public ulong OpposingPartyMember;
        public ulong QueueID;
        public bool TournamentRules;
        public uint OpposingPartyMemberVirtualRealmAddress;
        public uint OpposingPartyMemberCfgRealmID;
    }
}
