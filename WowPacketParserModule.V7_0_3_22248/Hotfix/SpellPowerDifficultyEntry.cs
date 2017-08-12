using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.SpellPowerDifficulty)]
    public class SpellPowerDifficultyEntry
    {
        public byte DifficultyID { get; set; }
        public byte PowerIndex { get; set; }
        public uint ID { get; set; }
    }
}