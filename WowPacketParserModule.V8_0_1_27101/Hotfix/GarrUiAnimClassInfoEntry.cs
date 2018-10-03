using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.GarrUiAnimClassInfo, HasIndexInData = false)]
    public class GarrUiAnimClassInfoEntry
    {
        public byte GarrClassSpecID { get; set; }
        public byte MovementType { get; set; }
        public float ImpactDelaySecs { get; set; }
        public uint CastKit { get; set; }
        public uint ImpactKit { get; set; }
        public uint TargetImpactKit { get; set; }
    }
}
