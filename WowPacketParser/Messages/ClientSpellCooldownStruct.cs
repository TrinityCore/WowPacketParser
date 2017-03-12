using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellCooldownStruct
    {
        public uint SrecID;
        public uint ForcedCooldown;
    }
}
