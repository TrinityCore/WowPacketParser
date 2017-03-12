using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientSetSpellCharges
    {
        public bool IsPet;
        public float Count;
        public int Category;
    }
}
