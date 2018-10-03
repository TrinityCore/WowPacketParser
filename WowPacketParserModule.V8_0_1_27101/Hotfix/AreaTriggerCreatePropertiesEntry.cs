using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AreaTriggerCreateProperties, HasIndexInData = false)]
    public class AreaTriggerCreatePropertiesEntry
    {
        public sbyte ShapeType { get; set; }
        public short StartShapeID { get; set; }
    }
}
