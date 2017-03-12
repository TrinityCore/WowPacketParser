using System.Collections.Generic;
using WowPacketParser.Misc;

namespace WowPacketParser.Messages
{
    public unsafe struct ClientPetitionShowSignatures
    {
        public List<PetitionSignature> Signatures;
        public int PetitionID;
        public ulong Item;
        public ulong Owner;
    }
}
