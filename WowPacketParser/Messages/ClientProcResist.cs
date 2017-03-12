using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientProcResist
    {
        public ulong Target;
        public float Needed; // Optional
        public int SpellID;
        public float Rolled; // Optional
        public ulong Caster;
    }
}
