using WowPacketParser.Enums;
using WowPacketParser.Hotfix;

namespace WowPacketParserModule.V7_0_3_22248.Hotfix
{
    [HotfixStructure(DB2Hash.GuildPerkSpells, HasIndexInData = false)]
    public class GuildPerkSpellsEntry
    {
        public uint SpellID { get; set; }
    }
}