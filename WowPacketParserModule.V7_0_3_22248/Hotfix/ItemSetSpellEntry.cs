using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.ItemSetSpell, HasIndexInData = false)]
    public class ItemSetSpellEntry
    {
        public uint SpellID { get; set; }
        public ushort ItemSetID { get; set; }
        public ushort ChrSpecID { get; set; }
        public byte Threshold { get; set; }
    }
}