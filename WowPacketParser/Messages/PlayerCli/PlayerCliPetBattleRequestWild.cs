using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliPetBattleRequestWild
    {
        public PetBattleLocations Location;
        public ulong TargetGUID;
    }
}
