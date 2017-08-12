using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_2_0_23826.Hotfix
{
    [HotfixStructure(DB2Hash.QuestPackageItem, ClientVersionBuild.V7_2_0_23826, HasIndexInData = false)]
    public class QuestPackageItemEntry
    {
        public uint ItemID { get; set; }
        public ushort QuestPackageID { get; set; }
        public byte FilterType { get; set; }
        public uint ItemCount { get; set; }
    }
}