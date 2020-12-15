using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemSpecOverride, HasIndexInData = false)]
    public class ItemSpecOverrideEntry
    {
        public ushort SpecID { get; set; }
        public uint ItemID { get; set; }
    }
}
