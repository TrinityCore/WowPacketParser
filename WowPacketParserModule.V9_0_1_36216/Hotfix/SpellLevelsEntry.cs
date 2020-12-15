using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SpellLevels, HasIndexInData = false)]
    public class SpellLevelsEntry
    {
        public byte DifficultyID { get; set; }
        public short MaxLevel { get; set; }
        public byte MaxPassiveAuraLevel { get; set; }
        public int BaseLevel { get; set; }
        public int SpellLevel { get; set; }
        public uint SpellID { get; set; }
    }
}
