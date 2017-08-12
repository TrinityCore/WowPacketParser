using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ItemXBonusTree, HasIndexInData = false)]
    public class ItemXBonusTreeEntry
    {
        public uint ItemID { get; set; }
        public ushort BonusTreeID { get; set; }
    }
}