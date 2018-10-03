using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.OccluderLocation)]
    public class OccluderLocationEntry
    {
        [HotfixArray(3)]
        public float[] Pos { get; set; }
        [HotfixArray(3)]
        public float[] Rot { get; set; }
        public int ID { get; set; }
        public int MapID { get; set; }
    }
}
