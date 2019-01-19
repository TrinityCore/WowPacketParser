using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.BattlePetBreedQuality, HasIndexInData = false)]
    public class BattlePetBreedQualityEntry
    {
        public float StateMultiplier { get; set; }
        public byte QualityEnum { get; set; }
    }
}
