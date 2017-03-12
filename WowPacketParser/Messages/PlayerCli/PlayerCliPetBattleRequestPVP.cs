using WowPacketParser.Messages.Submessages;

namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliPetBattleRequestPVP
    {
        public PetBattleLocations Location;
        public ulong OpponentCharacterID;
    }
}
