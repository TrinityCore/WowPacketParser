using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_1_0_39185.Hotfix
{
    [HotfixStructure(DB2Hash.TransmogSetItem, ClientVersionBuild.V9_1_0_39185, HasIndexInData = false)]
    public class TransmogSetItemEntry
    {
        public uint TransmogSetID { get; set; }
        public uint ItemModifiedAppearanceID { get; set; }
        public int Flags { get; set; }
    }
}
