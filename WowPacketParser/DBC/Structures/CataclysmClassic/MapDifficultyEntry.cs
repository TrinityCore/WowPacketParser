using DBFileReaderLib.Attributes;

namespace WowPacketParser.DBC.Structures.CataclysmClassic
{
    [DBFile("MapDifficulty")]
    public sealed class MapDifficultyEntry
    {
        [Index(true)]
        public uint ID;
        public string Message;
        public uint ItemContextPickerID;
        public int ContentTuningID;
        public int ItemContext;
        public byte DifficultyID;
        public byte LockID;
        public byte ResetInterval;
        public byte MaxPlayers;
        public byte Flags;
        public uint MapID;
    }
}
