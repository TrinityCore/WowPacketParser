using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AreaTriggerSphere, HasIndexInData = false)]
    public class AreaTriggerSphereEntry
    {
        public float MaxRadius { get; set; }
    }
}
