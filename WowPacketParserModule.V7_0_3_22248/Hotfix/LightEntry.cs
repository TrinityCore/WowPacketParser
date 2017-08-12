using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Light, HasIndexInData = false)]
    public class LightEntry
    {
        [HotfixArray(3)]
        public float[] Pos { get; set; }
        public float FalloffStart { get; set; }
        public float FalloffEnd { get; set; }
        public ushort MapID { get; set; }
        [HotfixArray(8)]
        public ushort[] LightParamsID { get; set; }
    }
}