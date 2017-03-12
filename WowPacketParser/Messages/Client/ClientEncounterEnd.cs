namespace WowPacketParser.Messages.Client
{
    public unsafe struct ClientEncounterEnd
    {
        public int GroupSize;
        public bool Success;
        public int DifficultyID;
        public int EncounterID;
    }
}
