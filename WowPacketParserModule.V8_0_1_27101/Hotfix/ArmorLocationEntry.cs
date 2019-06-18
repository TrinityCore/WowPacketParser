using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ArmorLocation, HasIndexInData = false)]
    public class ArmorLocationEntry
    {
        public float Clothmodifier { get; set; }
        public float Leathermodifier { get; set; }
        public float Chainmodifier { get; set; }
        public float Platemodifier { get; set; }
        public float Modifier { get; set; }
    }
}
