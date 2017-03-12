using System.Collections.Generic;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientSpellCooldown
    {
        public List<ClientSpellCooldownStruct> SpellCooldowns;
        public ulong Caster;
        public byte Flags;
    }
}
