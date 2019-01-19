using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.QuestPackageItem, HasIndexInData = false)]
    public class QuestPackageItemEntry
    {
        public ushort PackageID { get; set; }
        public int ItemID { get; set; }
        public uint ItemQuantity { get; set; }
        public byte DisplayType { get; set; }
    }
}
