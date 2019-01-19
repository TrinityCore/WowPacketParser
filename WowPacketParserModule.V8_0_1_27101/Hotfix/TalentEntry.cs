using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.Talent, HasIndexInData = false)]
    public class TalentEntry
    {
        public string Description { get; set; }
        public byte TierID { get; set; }
        public byte Flags { get; set; }
        public byte ColumnIndex { get; set; }
        public byte ClassID { get; set; }
        public ushort SpecID { get; set; }
        public uint SpellID { get; set; }
        public uint OverridesSpellID { get; set; }
        [HotfixArray(2)]
        public byte[] CategoryMask { get; set; }
    }
}
