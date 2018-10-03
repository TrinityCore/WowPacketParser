using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.MapLoadingScreen, HasIndexInData = false)]
    public class MapLoadingScreenEntry
    {
        [HotfixArray(2)]
        public float[] Min { get; set; }
        [HotfixArray(2)]
        public float[] Max { get; set; }
        public int LoadingScreenID { get; set; }
        public int OrderIndex { get; set; }
        public int MapID { get; set; }
    }
}
