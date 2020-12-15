using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.PvpDifficulty, HasIndexInData = false)]
    public class PVPDifficultyEntry
    {
        public byte RangeIndex { get; set; }
        public byte MinLevel { get; set; }
        public byte MaxLevel { get; set; }
        public ushort MapID { get; set; }
    }
}
