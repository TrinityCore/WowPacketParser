using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellModifier
    {
        public byte ModIndex;
        public List<ClientSpellModifierData> ModifierData;
    }
}
