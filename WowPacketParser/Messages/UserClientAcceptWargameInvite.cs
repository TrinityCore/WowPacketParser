using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientAcceptWargameInvite
    {
        public ulong OpposingPartyMember;
        public ulong QueueID;
        public bool Accept;
    }
}
