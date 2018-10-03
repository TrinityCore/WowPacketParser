using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AreaConditionalData)]
    public class AreaConditionalDataEntry
    {
        public string AreaName { get; set; }
        public int ID { get; set; }
        public int OrderIndex { get; set; }
        public int PlayerConditionID { get; set; }
        public int AreaID { get; set; }
    }
}
