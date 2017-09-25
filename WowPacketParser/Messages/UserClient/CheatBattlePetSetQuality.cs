namespace WowPacketParser.Messages.UserClient
{
    public unsafe struct CheatBattlePetSetQuality
    {
        public ushort BreedQuality;
        public ulong BattlePetGUID;
        public bool AllPetsInJournal;
    }
}
