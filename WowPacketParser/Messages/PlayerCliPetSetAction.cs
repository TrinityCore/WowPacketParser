using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliPetSetAction
    {
        public ulong PetGUID;
        public uint Action;
        public uint Index;
    }
}
