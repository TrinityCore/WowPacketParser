using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PvpDifficulty, HasIndexInData = false)]
    public class PvpDifficultyEntry
    {
        public byte RangeIndex { get; set; }
        public byte MinLevel { get; set; }
        public byte MaxLevel { get; set; }
        public ushort MapID { get; set; }
    }
}
