namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientAddBattlePet
    {
        public bool AllSpecies;
        public int SpeciesID;
        public int CreatureID;
        public bool IgnoreMaxPetRestriction;
    }
}
