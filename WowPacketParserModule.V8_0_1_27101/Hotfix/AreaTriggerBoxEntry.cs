using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.AreaTriggerBox, HasIndexInData = false)]
    public class AreaTriggerBoxEntry
    {
        [HotfixArray(3)]
        public float[] Extents { get; set; }
    }
}
