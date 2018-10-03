using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AreaTriggerCylinder, HasIndexInData = false)]
    public class AreaTriggerCylinderEntry
    {
        public float Radius { get; set; }
        public float Height { get; set; }
        public float ZOffset { get; set; }
    }
}
