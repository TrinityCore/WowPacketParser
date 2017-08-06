using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.TransmogSetItem, HasIndexInData = false)]
    public class TransmogSetItemEntry
    {
        public uint TransmogSetID { get; set; }
        public uint ItemModifiedAppearanceID { get; set; }
        public uint Unknown { get; set; }
    }
}
