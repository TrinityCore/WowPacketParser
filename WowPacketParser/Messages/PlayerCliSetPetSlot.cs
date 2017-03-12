using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliSetPetSlot
    {
        public ulong StableMaster;
        public uint PetNumber;
        public byte DestSlot;
    }
}
