using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiWidgetDataSource, HasIndexInData = false)]
    public class UiWidgetDataSourceEntry
    {
        public ushort SourceID { get; set; }
        public sbyte SourceType { get; set; }
        public ushort ReqID { get; set; }
        public ushort ParentWidgetID { get; set; }
    }
}
