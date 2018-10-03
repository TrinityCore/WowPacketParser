using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Location, HasIndexInData = false)]
    public class LocationEntry
    {
        [HotfixArray(3)]
        public float[] Pos { get; set; }
        [HotfixArray(3)]
        public float[] Rot { get; set; }
    }
}
