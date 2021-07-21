using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.BarberShopStyle, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class BarberShopStyleEntry
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public byte Type { get; set; }
        public float CostModifier { get; set; }
        public byte Race { get; set; }
        public byte Sex { get; set; }
        public byte Data { get; set; }
    }
}
