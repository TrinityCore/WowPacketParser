using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PetSpellHistory
    {
        public int CategoryID;
        public int RecoveryTime;
        public sbyte ConsumedCharges;
    }
}
