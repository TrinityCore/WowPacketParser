using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.TransmogSetItem, ClientVersionBuild.V9_0_1_36216, ClientVersionBuild.V9_1_0_39185)]
    public class TransmogSetItemEntry
    {
        public uint ID { get; set; }
        public uint TransmogSetID { get; set; }
        public uint ItemModifiedAppearanceID { get; set; }
        public int Flags { get; set; }
    }
}
