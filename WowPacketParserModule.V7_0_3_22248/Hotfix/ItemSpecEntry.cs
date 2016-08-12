using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ItemSpec, HasIndexInData = false)]
    public class ItemSpecEntry
    {
        public ushort SpecID { get; set; }
        public byte MinLevel { get; set; }
        public byte MaxLevel { get; set; }
        public byte ItemType { get; set; }
        public byte PrimaryStat { get; set; }
        public byte SecondaryStat { get; set; }
    }
}