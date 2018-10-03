using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V8_0_1_27101.Hotfix
{
    [HotfixStructure(DB2Hash.CastableRaidBuffs, HasIndexInData = false)]
    public class CastableRaidBuffsEntry
    {
        public uint CastingSpellID { get; set; }
        public int SpellID { get; set; }
    }
}
