using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetPctSpellModifier
    {
        public List<ClientSpellModifier> Modifiers;
    }
}
