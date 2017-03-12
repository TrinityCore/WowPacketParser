using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCancelSpellVisual
    {
        public ulong Source;
        public int SpellVisualID;
    }
}
