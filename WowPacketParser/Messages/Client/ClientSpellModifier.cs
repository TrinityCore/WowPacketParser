using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellModifier
    {
        public byte ModIndex;
        public List<ClientSpellModifierData> ModifierData;
    }
}
