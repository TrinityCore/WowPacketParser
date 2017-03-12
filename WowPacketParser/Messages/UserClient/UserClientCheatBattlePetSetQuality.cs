namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct UserClientCheatBattlePetSetQuality
    {
        public ushort BreedQuality;
        public ulong BattlePetGUID;
        public bool AllPetsInJournal;
    }
}
