using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDestructibleBuildingDamage
    {
        public ulong Target;
        public ulong Caster;
        public ulong Owner;
        public int Damage;
        public int SpellID;
    }
}
