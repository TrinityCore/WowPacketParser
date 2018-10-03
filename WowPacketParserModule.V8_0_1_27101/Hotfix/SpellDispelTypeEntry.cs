using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.SpellDispelType, HasIndexInData = false)]
    public class SpellDispelTypeEntry
    {
        public string Name { get; set; }
        public string InternalName { get; set; }
        public byte ImmunityPossible { get; set; }
        public byte Mask { get; set; }
    }
}
