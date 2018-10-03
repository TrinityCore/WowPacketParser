using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.HelmetGeosetVisData, HasIndexInData = false)]
    public class HelmetGeosetVisDataEntry
    {
        [HotfixArray(9)]
        public int[] HideGeoset { get; set; }
    }
}
