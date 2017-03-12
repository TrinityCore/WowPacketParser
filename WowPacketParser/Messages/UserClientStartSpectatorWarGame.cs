using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientStartSpectatorWarGame
    {
        public uint OpposingPartyMemberCfgRealmID1;
        public ulong OpposingPartyMember1;
        public uint OpposingPartyMemberVirtualRealmAddress1;
        public ulong QueueID;
        public uint OpposingPartyMemberCfgRealmID2;
        public uint OpposingPartyMemberVirtualRealmAddress2;
        public ulong OpposingPartyMember2;
        public bool TournamentRules;
    }
}
