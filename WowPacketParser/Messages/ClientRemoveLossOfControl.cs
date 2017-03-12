using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientRemoveLossOfControl
    {
        public int SpellID;
        public ulong Caster;
        public int Type;
    }
}
