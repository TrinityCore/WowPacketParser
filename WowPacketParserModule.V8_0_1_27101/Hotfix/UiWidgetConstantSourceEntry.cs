using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiWidgetConstantSource, HasIndexInData = false)]
    public class UiWidgetConstantSourceEntry
    {
        public ushort ReqID { get; set; }
        public int Value { get; set; }
        public ushort ParentWidgetID { get; set; }
    }
}
