namespace WowPacketParser.Messages.PlayerCli
{
    public unsafe struct PlayerCliSetEncounterVar
    {
        public int DungeonEncounterID;
        public string VarName;
        public bool Attempt;
        public float VarValue;
    }
}
