using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.UiWidget, HasIndexInData = false)]
    public class UiWidgetEntry
    {
        public string WidgetTag { get; set; }
        public ushort ParentSetID { get; set; }
        public int VisID { get; set; }
        public int MapID { get; set; }
        public int PlayerConditionID { get; set; }
        public uint OrderIndex { get; set; }
    }
}
