using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.NumTalentsAtLevel)]
    public class NumTalentsAtLevelEntry
    {
        public uint ID { get; set; }
        public int NumTalents { get; set; }
        public int NumTalentsDeathKnight { get; set; }
        public int NumTalentsDemonHunter { get; set; }
    }
}
