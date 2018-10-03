using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.LiquidObject, HasIndexInData = false)]
    public class LiquidObjectEntry
    {
        public float FlowDirection { get; set; }
        public float FlowSpeed { get; set; }
        public short LiquidTypeID { get; set; }
        public byte Fishable { get; set; }
        public byte Reflection { get; set; }
    }
}
