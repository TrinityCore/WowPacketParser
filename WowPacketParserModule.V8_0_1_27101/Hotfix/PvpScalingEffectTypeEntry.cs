using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.PvpScalingEffectType, HasIndexInData = false)]
    public class PvpScalingEffectTypeEntry
    {
        public string Name { get; set; }
    }
}
