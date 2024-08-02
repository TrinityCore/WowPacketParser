using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.TheWarWithin
{
    [DBFile("MapDifficulty")]

    public class MapDifficultyEntry
    {
        [Index(true)]
        public uint ID;
        public string Message;
        public int DifficultyID;
        public int LockID;
        public byte ResetInterval;
        public int MaxPlayers;
        public byte ItemContext;
        public int ItemContextPickerID;
        public int Flags;
        public int ContentTuningID;
        public int MapID;
    }
}
