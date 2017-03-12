using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CliMailAttachedItemEnchant
    {
        public int Enchant;
        public uint Duration;
        public int Charges;
    }
}
