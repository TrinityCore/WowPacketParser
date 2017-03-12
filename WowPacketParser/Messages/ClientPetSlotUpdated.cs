using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetSlotUpdated
    {
        public int PetSlotA;
        public int PetNumberB;
        public int PetNumberA;
        public int PetSlotB;
    }
}
