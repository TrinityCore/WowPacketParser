using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientCancelSpellVisualKit
    {
        public ulong Source;
        public int SpellVisualKitID;
    }
}
