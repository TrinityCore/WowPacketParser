using System.Collections.Generic;
using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPetitionShowSignatures
    {
        public List<PetitionSignature> Signatures;
        public int PetitionID;
        public ulong Item;
        public ulong Owner;
    }
}
