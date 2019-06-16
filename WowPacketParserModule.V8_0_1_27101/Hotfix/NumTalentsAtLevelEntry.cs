using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
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
