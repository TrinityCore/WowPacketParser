using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ArmorLocation, HasIndexInData = false)]
    public class ArmorLocationEntry
    {
        public float ClothModifier { get; set; }
        public float LeatherModifier { get; set; }
        public float ChainModifier { get; set; }
        public float PlateModifier { get; set; }
        public float Modifier { get; set; }
    }
}
