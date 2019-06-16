using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Light, HasIndexInData = false)]
    public class LightEntry
    {
        [HotfixArray(3, true)]
        public float[] GameCoords { get; set; }
        public float GameFalloffStart { get; set; }
        public float GameFalloffEnd { get; set; }
        public short ContinentID { get; set; }
        [HotfixArray(8)]
        public ushort[] LightParamsID { get; set; }
    }
}
