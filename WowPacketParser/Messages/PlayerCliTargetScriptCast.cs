using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliTargetScriptCast
    {
        public ulong TargetGUID;
        public int SpellID;
    }
}
