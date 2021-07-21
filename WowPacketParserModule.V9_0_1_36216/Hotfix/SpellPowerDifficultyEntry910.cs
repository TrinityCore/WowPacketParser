using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.SpellPowerDifficulty, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class SpellPowerDifficultyEntry
    {
        public byte DifficultyID { get; set; }
        public byte OrderIndex { get; set; }
    }
}
