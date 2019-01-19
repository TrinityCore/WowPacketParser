using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.ItemSpecOverride, HasIndexInData = false)]
    public class ItemSpecOverrideEntry
    {
        public ushort SpecID { get; set; }
        public int ItemID { get; set; }
    }
}
