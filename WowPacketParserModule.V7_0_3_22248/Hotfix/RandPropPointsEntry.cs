using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.RandPropPoints, HasIndexInData = false)]
    public class RandPropPointsEntry
    {
        [HotfixArray(5)]
        public uint[] EpicPropertiesPoints { get; set; }
        [HotfixArray(5)]
        public uint[] RarePropertiesPoints { get; set; }
        [HotfixArray(5)]
        public uint[] UncommonPropertiesPoints { get; set; }
    }
}