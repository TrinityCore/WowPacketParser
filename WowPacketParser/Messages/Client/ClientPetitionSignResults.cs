using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPetitionSignResults
    {
        public ulong Player;
        public PetitionError Error;
        public ulong Item;
    }
}
