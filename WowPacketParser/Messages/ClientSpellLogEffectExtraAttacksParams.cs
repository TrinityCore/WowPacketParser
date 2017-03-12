using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSpellLogEffectExtraAttacksParams
    {
        public ulong Victim;
        public uint NumAttacks;
    }
}
