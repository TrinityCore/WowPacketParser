using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellLevels, HasIndexInData = false)]
    public class SpellLevelsEntry
    {
        public byte DifficultyID { get; set; }
        public short BaseLevel { get; set; }
        public short MaxLevel { get; set; }
        public short SpellLevel { get; set; }
        public byte MaxPassiveAuraLevel { get; set; }
        public int SpellID { get; set; }
    }
}
