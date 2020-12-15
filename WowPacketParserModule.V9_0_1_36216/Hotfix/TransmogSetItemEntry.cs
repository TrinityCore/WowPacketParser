using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.TransmogSetItem)]
    public class TransmogSetItemEntry
    {
        public uint ID { get; set; }
        public uint TransmogSetID { get; set; }
        public uint ItemModifiedAppearanceID { get; set; }
        public int Flags { get; set; }
    }
}
