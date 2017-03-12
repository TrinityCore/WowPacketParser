using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct PlayerCliLearnPetSpecializationGroup
    {
        public ulong PetGUID;
        public uint SpecGroupIndex;
    }
}
