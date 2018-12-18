namespace WowPacketParser.DBC.Structures.BattleForAzeroth
{
    [DBFile("MapDifficulty")]

    public class MapDifficultyEntry
    {
        public string Message;
        public uint ItemContextPickerID;
        public int Unk;
        public byte DifficultyID;
        public byte LockID;
        public byte ResetInterval;
        public byte MaxPlayers;
        public byte ItemContext;
        public byte Flags;
        public ushort MapID;
    }
}
