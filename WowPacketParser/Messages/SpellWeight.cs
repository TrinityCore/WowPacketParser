using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct SpellWeight
    {
        public SpellweightTokenTypes Type;
        public int ID;
        public uint Quantity;
    }
}
