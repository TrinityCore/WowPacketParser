using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.Talent, HasIndexInData = false)]
    public class TalentEntry
    {
        public uint SpellID { get; set; }
        public uint OverridesSpellID { get; set; }
        public string Description { get; set; }
        public ushort SpecID { get; set; }
        public byte TierID { get; set; }
        public byte ColumnIndex { get; set; }
        public byte Flags { get; set; }
        [HotfixArray(2)]
        public byte[] CategoryMask { get; set; }
        public byte ClassID { get; set; }
    }
}