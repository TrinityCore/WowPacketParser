using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientSilencePartyTalker
    {
        public bool Silence;
        public ulong Target;
        public byte PartyIndex;
    }
}
