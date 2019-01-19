using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellPowerDifficulty)]
    public class SpellPowerDifficultyEntry
    {
        public int ID { get; set; }
        public byte DifficultyID { get; set; }
        public byte OrderIndex { get; set; }
    }
}
