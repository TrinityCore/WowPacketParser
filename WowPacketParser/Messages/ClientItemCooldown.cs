using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientItemCooldown
    {
        public ulong ItemGuid;
        public uint SpellID;
    }
}
