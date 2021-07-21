using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.NumTalentsAtLevel, ClientVersionBuild.V9_0_1_36216, ClientVersionBuild.V9_1_0_39185)]
    public class NumTalentsAtLevelEntry
    {
        public uint ID { get; set; }
        public int NumTalents { get; set; }
        public int NumTalentsDeathKnight { get; set; }
        public int NumTalentsDemonHunter { get; set; }
    }
}
