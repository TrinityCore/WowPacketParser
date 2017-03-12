using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
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
