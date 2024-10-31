using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.AreaPoiState)]
    public class AreaPOIStateEntry
    {
        public uint ID { get; set; }
        public uint AreaPoiID { get; set; }
        public uint State { get; set; }
        public uint Icon { get; set; }
        public string Description { get; set; }
    }
}