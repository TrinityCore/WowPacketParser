using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.MapDifficulty, HasIndexInData = false)]
    public class MapDifficultyEntry
    {
        public string Message { get; set; }
        public int DifficultyID { get; set; }
        public int LockID { get; set; }
        public sbyte ResetInterval { get; set; }
        public int MaxPlayers { get; set; }
        public int ItemContext { get; set; }
        public int ItemContextPickerID { get; set; }
        public int Flags { get; set; }
        public int ContentTuningID { get; set; }
        public uint MapID { get; set; }
    }
}
