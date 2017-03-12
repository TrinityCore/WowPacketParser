using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCheckWargameEntry
    {
        public ulong QueueID;
        public ulong OpposingPartyBnetAccountID;
        public ServerSpec OpposingPartyUserServer;
        public ulong OpposingPartyMember;
        public UnixTime TimeoutSeconds;
        public bool TournamentRules;
    }
}
