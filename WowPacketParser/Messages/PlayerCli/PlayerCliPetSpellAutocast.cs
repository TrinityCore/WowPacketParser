namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliPetSpellAutocast
    {
        public ulong PetGUID;
        public bool AutocastEnabled;
        public int SpellID;
    }
}
