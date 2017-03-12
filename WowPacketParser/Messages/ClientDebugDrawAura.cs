using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDebugDrawAura
    {
        public ulong Caster;
        public int SpellID;
        public Vector3 Position;
    }
}
