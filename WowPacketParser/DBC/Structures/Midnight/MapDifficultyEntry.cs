using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.Midnight
{
    [DBFile("MapDifficulty")]
    public sealed class MapDifficultyEntry
    {
        public string Message;
        [Index(false)]
        public uint ID;
        public short DifficultyID;
        public int LockID;
        public byte ResetInterval;
        public int MaxPlayers;
        public byte ItemContext;
        public int ItemContextPickerID;
        public int Flags;
        public int ContentTuningID;
        public int WorldStateExpressionID;
        [NonInlineRelation(typeof(uint))]
        public int MapID;
    }
}
