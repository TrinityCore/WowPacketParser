using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetAdded
    {
        public string Name;
        public int CreatureID;
        public int Level;
        public uint PetNumber;
        public int DisplayID;
        public byte Flags;
        public int PetSlot;
    }
}
