namespace WowPacketParser.DBC.Structures
{
    public sealed class MapDifficultyEntry
    {
        public uint ID;
        public int MapID;
        public int DifficultyID;
        public string Message_lang;
        public uint RaidDuration;
        public uint MaxPlayers;
        public uint LockID;
        public uint ItemBonusTreeModID;
    }
}
