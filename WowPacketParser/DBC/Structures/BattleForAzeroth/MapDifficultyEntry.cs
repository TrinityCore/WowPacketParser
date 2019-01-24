namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("MapDifficulty")]

    public class MapDifficultyEntry
    {
        public string Message;
        public uint ItemContextPickerID;
        public int ContentTuningID;
        public byte DifficultyID;
        public int LockID;
        public int ResetInterval;
        public int MaxPlayers;
        public int ItemContext;
        public int Flags;
        public int MapID;
    }
}
