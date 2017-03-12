using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliDebugPassiveAura
    {
        public int SpellID;
        public ulong Target;
        public bool Add;
    }
}
