using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellCooldown
    {
        public List<ClientSpellCooldownStruct> SpellCooldowns;
        public ulong Caster;
        public byte Flags;
    }
}
