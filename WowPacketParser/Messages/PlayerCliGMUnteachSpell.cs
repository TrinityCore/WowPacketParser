using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliGMUnteachSpell
    {
        public ulong TargetGUID;
        public int SpellID;
    }
}
