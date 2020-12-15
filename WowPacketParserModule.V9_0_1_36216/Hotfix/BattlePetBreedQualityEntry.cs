using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetBreedQuality, HasIndexInData = false)]
    public class BattlePetBreedQualityEntry
    {
        public float StateMultiplier { get; set; }
        public byte QualityEnum { get; set; }
    }
}
