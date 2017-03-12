using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGMTeachSpell
    {
        public ulong TargetGUID;
        public int SpellID;
    }
}
