using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ItemSpecOverride, HasIndexInData = false)]
    public class ItemSpecOverrideEntry
    {
        public uint ItemID { get; set; }
        public ushort SpecID { get; set; }
    }
}