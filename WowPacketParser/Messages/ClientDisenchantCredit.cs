using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientDisenchantCredit
    {
        public ulong Disenchanter;
        public ItemInstance Item;
    }
}
