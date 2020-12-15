using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
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
