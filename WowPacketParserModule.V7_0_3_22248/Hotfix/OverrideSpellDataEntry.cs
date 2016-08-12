using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.OverrideSpellData, HasIndexInData = false)]
    public class OverrideSpellDataEntry
    {
        [HotfixArray(10)]
        public uint[] SpellID { get; set; }
        public uint PlayerActionbarFileDataID { get; set; }
        public byte Flags { get; set; }
    }
}