using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemSubClass, HasIndexInData = false)]
    public class ItemSubClassEntry
    {
        public string DisplayName { get; set; }
        public string VerboseName { get; set; }
        public sbyte ClassID { get; set; }
        public sbyte SubClassID { get; set; }
        public byte AuctionHouseSortOrder { get; set; }
        public sbyte PrerequisiteProficiency { get; set; }
        public short Flags { get; set; }
        public sbyte DisplayFlags { get; set; }
        public sbyte WeaponSwingSize { get; set; }
        public sbyte PostrequisiteProficiency { get; set; }
    }
}
