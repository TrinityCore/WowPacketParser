using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliTargetCast
    {
        public int SpellID;
        public ulong CastTarget;
        public ulong TargetGUID;
        public bool CreatureAICast;
    }
}
