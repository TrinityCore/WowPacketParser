using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSetPartyAssignment
    {
        public byte PartyIndex;
        public ulong Target;
        public byte Assignment;
        public bool Set;
    }
}
