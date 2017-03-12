using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PetSpellCooldown
    {
        public int SpellID;
        public int Duration;
        public int CategoryDuration;
        public ushort Category;
    }
}
