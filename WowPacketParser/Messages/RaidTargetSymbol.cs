using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct RaidTargetSymbol
    {
        public ulong Target;
        public byte Symbol;
    }
}
