using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct CategoryCooldown
    {
        public int Category;
        public int ModCooldown;
    }
}
