using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_1_0_28724.Hotfix
{
    [HotfixStructure(DB2Hash.MapDifficulty, ClientVersionBuild.V8_1_0_28724, HasIndexInData = false)]
    public class MapDifficultyEntry
    {
        public string Message { get; set; }
        public uint ItemContextPickerID { get; set; }
        public int ContentTuningID { get; set; }
        public sbyte DifficultyID { get; set; }
        public int LockID { get; set; }
        public int ResetInterval { get; set; }
        public int MaxPlayers { get; set; }
        public int ItemContext { get; set; }
        public int Flags { get; set; }
        public uint MapID { get; set; }
    }
}
