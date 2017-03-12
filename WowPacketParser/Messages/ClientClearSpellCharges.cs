using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientClearSpellCharges
    {
        public ulong Unit;
        public int Category;
    }
}
