using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct UserClientUpdateRaidTarget
    {
        public ulong Target;
        public byte PartyIndex;
        public byte Symbol;
    }
}
