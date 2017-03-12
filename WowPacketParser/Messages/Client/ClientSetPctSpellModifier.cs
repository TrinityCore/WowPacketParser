using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSetPctSpellModifier
    {
        public List<ClientSpellModifier> Modifiers;
    }
}
