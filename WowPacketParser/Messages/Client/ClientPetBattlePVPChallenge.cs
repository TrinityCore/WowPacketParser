using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientPetBattlePVPChallenge
    {
        public ulong ChallengerGUID;
        public PetBattleLocations Location;
    }
}
