using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V9_0_1_36216.Hotfix
{
    [HotfixStructure(DB2Hash.ItemSetSpell, HasIndexInData = false)]
    public class ItemSetSpellEntry
    {
        public ushort ChrSpecID { get; set; }
        public uint SpellID { get; set; }
        public byte Threshold { get; set; }
        public ushort ItemSetID { get; set; }
    }
}
