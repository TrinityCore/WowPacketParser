using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliNoSpellVariance
    {
        public ulong Target;
        public bool Enable;
    }
}
