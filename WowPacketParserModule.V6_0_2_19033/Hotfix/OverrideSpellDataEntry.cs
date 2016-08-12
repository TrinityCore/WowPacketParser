using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V6_0_2_19033.Hotfix
{
    [HotfixStructure(DB2Hash.OverrideSpellData)]
    public class OverrideSpellDataEntry
    {
        public uint ID { get; set; }
        [HotfixArray(10)]
        public uint[] SpellID { get; set; }
        public uint Flags { get; set; }
        public uint PlayerActionbarFileDataID { get; set; }
    }
}