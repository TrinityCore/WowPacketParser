using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.SpellPowerDifficulty, ClientVersionBuild.V9_0_1_36216, ClientVersionBuild.V9_1_0_39185)]
    public class SpellPowerDifficultyEntry
    {
        public uint ID { get; set; }
        public byte DifficultyID { get; set; }
        public byte OrderIndex { get; set; }
    }
}
